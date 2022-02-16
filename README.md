# storage.net-s3clone

Storage.Net Plugin for 3rd party S3 Clones

## Description

Plugin for [Storage.Net](https://github.com/aloneguid/storage) for 3rd party S3-compliant object storage

Supports S3 and FTP protocols

## Usage

```csharp
StorageFactory.Modules.UseS3CloneStorage();
IBlobStorage s3Storage = StorageFactory.Blobs.FromConnectionString("s3://uri=https://minio.host.com/;keyId=accessKey;key=secretKey;bucket=bucket-name;region=eu-central-1");
```
