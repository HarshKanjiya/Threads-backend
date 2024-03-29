﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;

namespace Thread.microservice.Utils
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
}
