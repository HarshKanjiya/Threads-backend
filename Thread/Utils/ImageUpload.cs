using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;

namespace Thread.microservice.Utils
{

    public class ImageUpload
    {
        private readonly Cloudinary cld;
        public ImageUpload(IOptions<CloudinarySettings> config)
        {

            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            cld = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> Upload(IFormFile file)
        {
            var UploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var UploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };

                UploadResult = await cld.UploadAsync(UploadParams);

            }
            return UploadResult;
        }

        public async Task<DeletionResult> DeleteImage(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await cld.DestroyAsync(deleteParams);
            return result;
        }
    }
}
