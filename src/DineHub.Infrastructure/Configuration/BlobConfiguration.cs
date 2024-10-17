namespace DineHub.Infrastructure.Configuration;

public class BlobConfiguration
{
    public string LogosContainerName { get; set; } = string.Empty;

    public string ConnectionString { get; set; } = string.Empty;

    public string AccountKey { get; set; } = string.Empty;

    public string AccountName { get; set; } = string.Empty;
}