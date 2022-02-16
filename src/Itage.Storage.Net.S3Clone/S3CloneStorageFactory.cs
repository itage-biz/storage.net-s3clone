using Amazon.S3;
using Storage.Net.Blobs;
using Storage.Net.ConnectionString;
using Storage.Net.Messaging;

// ReSharper disable once CheckNamespace
namespace Storage.Net;

/// <summary>
/// Storage net factory
/// </summary>
public static class Factory
{
    /// <summary>
    /// Register S3 Clone Module.
    /// </summary>
    public static IModulesFactory UseS3CloneStorage(this IModulesFactory factory)
    {
        return factory.Use(new MinioStorageModule());
    }

    private class MinioStorageModule : IExternalModule, IConnectionFactory
    {
        public IConnectionFactory ConnectionFactory => this;

        public IBlobStorage? CreateBlobStorage(StorageConnectionString connectionString)
        {
            if (connectionString.Prefix != "s3")
            {
                return null;
            }

            connectionString.GetRequired(KnownParameter.BucketName, true, out var bucket);
            connectionString.GetRequired("uri", true, out var uri);
            string? keyId = connectionString.Get(KnownParameter.KeyId);
            string? key = connectionString.Get(KnownParameter.KeyOrPassword);
            string? region = connectionString.Get(KnownParameter.Region);
            return StorageFactory.Blobs.AwsS3(keyId, key, null, bucket,
                new AmazonS3Config { ServiceURL = uri, ForcePathStyle = true, AuthenticationRegion = region });
        }

        public IMessenger? CreateMessenger(StorageConnectionString connectionString) => null;
    }
}