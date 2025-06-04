namespace Restaurants.Domain.Interfaces
{
    public interface IBlobStorageService
    {
        string? GetBlobSasUrl(string? blobUrl);
        Task<string> UploadToBlobStorageAsync(Stream data, string fileName);
    }
}
