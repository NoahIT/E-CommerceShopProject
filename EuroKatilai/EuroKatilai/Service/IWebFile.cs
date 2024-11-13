namespace EuroKatilai.Service
{
    public interface IWebFile
    {
        Task UploadImage(Stream fileStream, string filename);
        string GetWebFilename(string filename);
    }
}
