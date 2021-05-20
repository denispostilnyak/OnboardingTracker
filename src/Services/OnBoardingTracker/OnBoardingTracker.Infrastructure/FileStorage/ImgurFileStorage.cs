using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Microsoft.Extensions.Options;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Infrastructure.FileStorage.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Infrastructure.FileStorage
{
    public class ImgurFileStorage : IFileStorage
    {
        private readonly ImgurFileStorageOptions options;

        public ImgurFileStorage(IOptions<ImgurFileStorageOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<Uri> UploadFileAsync(Stream imgStream, string fileName, CancellationToken cancellationToken)
        {
            var apiClient = new ApiClient(options.ClientId);
            var httpClient = new HttpClient();

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var imageUpload = await imageEndpoint.UploadImageAsync(imgStream, cancellationToken: cancellationToken);
            return new Uri(imageUpload.Link);
        }
    }
}
