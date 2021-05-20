using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Microsoft.Extensions.Options;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Infrastructure.FileStorage.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Infrastructure.FileStorage
{
    public class AmazonS3FileStorage : IFileStorage
    {
        private readonly AmazonS3FileStorageOptions options;
        private readonly IAmazonS3 amazonS3Client;

        public AmazonS3FileStorage(IOptions<AmazonS3FileStorageOptions> storageOptions)
        {
            this.options = storageOptions.Value;
            var basicCredentials = new BasicAWSCredentials(options.AccessKeyId, options.SecretAccessKey);
            this.amazonS3Client = new AmazonS3Client(basicCredentials, RegionEndpoint.GetBySystemName(options.Region));
        }

        public async Task<Uri> UploadFileAsync(Stream imgStream, string fileName, CancellationToken cancellationToken)
        {
            var fileUploadUtility = new TransferUtility(amazonS3Client);

            var transferUploadRequest = new TransferUtilityUploadRequest
            {
                CannedACL = S3CannedACL.PublicRead,
                InputStream = imgStream,
                Key = fileName,
                BucketName = options.BucketName
            };
            await fileUploadUtility.UploadAsync(transferUploadRequest, cancellationToken);

            var signedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = options.BucketName,
                Key = fileName,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(options.SignedUrlExpirationInMinutes)
            };

            var url = amazonS3Client.GetPreSignedURL(signedUrlRequest);
            return new Uri(url);
        }
    }
}
