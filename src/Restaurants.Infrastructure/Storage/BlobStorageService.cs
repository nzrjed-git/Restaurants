﻿using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configuration;

namespace Restaurants.Infrastructure.Storage
{
    internal class BlobStorageService(
        IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
    {
        private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;

        public async Task<string> UploadToBlobStorageAsync(Stream data, string fileName)
        {
            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(data);

            var blobUrl = blobClient.Uri.ToString();
            return blobUrl;
        }

        public string? GetBlobSasUrl(string? blobUrl)
        {
            if (blobUrl == null) return null;

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _blobStorageSettings.LogosContainerName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
                BlobName = GetBlobNameFromUrl(blobUrl),
            };

            sasBuilder.SetPermissions(BlobAccountSasPermissions.Read);

            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

            var sasToken = sasBuilder
                .ToSasQueryParameters(
                    new StorageSharedKeyCredential(
                        blobServiceClient.AccountName, _blobStorageSettings.AccountKey))
                .ToString();

            var sasUrl = $"{blobUrl}?{sasToken}";
            return sasUrl;
        }
        private string GetBlobNameFromUrl(string blobUrl)
        {
            var uri = new Uri(blobUrl);
            return uri.Segments.Last();
        }
    }
}
