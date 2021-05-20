using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Options;
using OnBoardingTracker.Infrastructure.FileStorage.Abstractions;
using OnBoardingTracker.Infrastructure.FileStorage.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OnBoardingTracker.Infrastructure.FileStorage
{
    public class FirebaseFileStorage : IFileStorage
    {
        private readonly FirebaseFileStorageOptions options;
        private Task<string> authTokenAsyncFactory;

        public FirebaseFileStorage(IOptions<FirebaseFileStorageOptions> options)
        {
            this.options = options.Value;
        }

        public async Task<Uri> UploadFileAsync(Stream imgStream, string fileName, CancellationToken cancellationToken)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(options.ApiKey));
            var firebaseAuthToken = await authProvider.SignInAnonymouslyAsync();

            if (authTokenAsyncFactory == null)
            {
                authTokenAsyncFactory = Task.FromResult(firebaseAuthToken.FirebaseToken);
            }

            var upload = await new FirebaseStorage(
                options.Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => authTokenAsyncFactory,
                    ThrowOnCancel = false
                })
                .Child(options.Folder)
                .Child(fileName)
                .PutAsync(imgStream, cancellationToken);
            return new Uri(upload);
        }
    }
}
