using CloudinaryDotNet;
using CloudinaryDotNet.Actions;


namespace UserApi.microservice.Utils
{

    public class ImageUpload
    {
        private readonly Cloudinary cld;
        public ImageUpload()
        {

            var acc = new Account("dv9bbtpzb", "944233335766569", "bpgx_zqCoXD01YORG0tQwFpOPGk");
            cld = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> Upload(string file)
        {
            var UploadResult = new ImageUploadResult();

            var UploadParams = new ImageUploadParams
            {
                File = new FileDescription(@"" + file)
            };

            UploadResult = cld.Upload(UploadParams);


            return UploadResult;
        }

        public async Task<DeletionResult> DeleteImage(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await cld.DestroyAsync(deleteParams);
            return result;
        }
    }
    public class videoUploader
    {
        private readonly Cloudinary cld;
        public videoUploader()
        {

            var acc = new Account("deybbbbd4", "248857122428958", "xi_rXYFOyfyzhjpK29-elXwKgt0");
            cld = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> Upload(string file)
        {
            var UploadResult = new ImageUploadResult();


            var UploadParams = new ImageUploadParams
            {
                File = new FileDescription(@"" + file)
            };

            UploadResult = cld.Upload(UploadParams);


            return UploadResult;
        }

        public async Task<VideoUploadResult> videoUpload(string file)
        {
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(@"" + file)
            };

            var uploadResult = await cld.UploadAsync(uploadParams);

            return uploadResult;
        }

        public async Task<DeletionResult> DeleteImage(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await cld.DestroyAsync(deleteParams);
            return result;
        }

    }
}
