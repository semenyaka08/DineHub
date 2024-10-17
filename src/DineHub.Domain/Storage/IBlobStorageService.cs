namespace DineHub.Domain.Storage;

public interface IBlobStorageService
{
    public Task<string> UploadLogoAsync(Stream file,string fileName);

    public string? GetBlobSasUrl(string? url);
}