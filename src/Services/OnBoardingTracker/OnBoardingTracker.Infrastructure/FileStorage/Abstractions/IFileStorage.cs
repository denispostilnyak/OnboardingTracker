using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Infrastructure.FileStorage.Abstractions
{
    public interface IFileStorage
    {
        Task<Uri> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken);
    }
}
