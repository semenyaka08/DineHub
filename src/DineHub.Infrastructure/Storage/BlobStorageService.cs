using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using DineHub.Domain.Storage;
using DineHub.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace DineHub.Infrastructure.Storage;

public class BlobStorageService(IOptions<BlobConfiguration> blobConfiguration) : IBlobStorageService
{
    private readonly BlobConfiguration _blobConfiguration = blobConfiguration.Value;

    public async Task<string> UploadLogoAsync(Stream file, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_blobConfiguration.ConnectionString);
        
        var blobContainer = blobServiceClient.GetBlobContainerClient(_blobConfiguration.LogosContainerName);

        var blobClient = blobContainer.GetBlobClient(fileName);

        await blobClient.UploadAsync(file);

        return blobClient.Uri.ToString();
    }

    public string? GetBlobSasUrl(string? url)
    {
        if (url == null)
            return null;

        var builder = new BlobSasBuilder
        {
            StartsOn = DateTimeOffset.Now,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
            BlobContainerName = _blobConfiguration.LogosContainerName,
            BlobName = GetBlobName(url),
            Resource = "b",
        };
        
        builder.SetPermissions(BlobSasPermissions.Read);

        var sasToken = builder.ToSasQueryParameters(new StorageSharedKeyCredential(_blobConfiguration.AccountName, _blobConfiguration.AccountKey))
            .ToString();

        return $"{url}?{sasToken}";
    }

    private string GetBlobName(string blobUrl)
    {
        var uri = new Uri(blobUrl);

        return uri.Segments.Last();
    }
}