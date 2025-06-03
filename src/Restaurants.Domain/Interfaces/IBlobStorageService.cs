namespace Restaurants.Domain.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadToBlobStorageAsync(Stream data, string fileName);
    }
}
