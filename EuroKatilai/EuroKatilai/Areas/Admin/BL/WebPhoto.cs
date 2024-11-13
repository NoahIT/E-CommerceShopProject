using EuroKatilai.Middleware;
using EuroKatilai.Service;
using Microsoft.AspNetCore.Mvc;

namespace EuroKatilai.Areas.Admin.BL
{
    [Area("Admin")]
    [SiteAuthorize(RequireAdmin: true)]
    public class WebPhoto : IWebPhoto
    {
        private readonly IWebFile web;

        public WebPhoto(IWebFile web)
        {
            this.web = web;
        }

        public async Task<string?> UploadPhotoAndReturnFilePath(IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                string webFilename = web.GetWebFilename(photo.FileName);

                using (var fileStream = photo.OpenReadStream())
                {
                    await web.UploadImage(fileStream, webFilename);
                }

                return webFilename;
            }

            return null;
        }
    }
}
