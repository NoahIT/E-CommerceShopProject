namespace EuroKatilai.Areas.Admin.BL
{
    public interface IWebPhoto
    {
        Task<string?> UploadPhotoAndReturnFilePath(IFormFile photo);
    }
}
