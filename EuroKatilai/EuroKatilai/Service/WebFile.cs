using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Security.Cryptography;
using System.Text;

namespace EuroKatilai.Service
{
    public class WebFile : IWebFile
    {
        const string FOLDER_PREFIX = "./wwwroot/";

        public WebFile()
        {
        }
        public string GetWebFilename(string filename)
        {
            string dir = GetWebFileFolder(filename);

            CreateFolder(FOLDER_PREFIX + dir);

            return dir + "/" + Path.GetFileNameWithoutExtension(filename) + ".jpg";
        }

        public string GetWebFileFolder(string filename)
        {
            MD5 md5hash = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(filename);
            byte[] hashBytes = md5hash.ComputeHash(inputBytes);

            string hash = Convert.ToHexString(hashBytes);

            return "images/" + hash.Substring(0, 2) + "/" +
                   hash.Substring(0, 4);
        }

        public void CreateFolder(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public async Task UploadImage(Stream fileStream, string filename)
        {
            using (Image image = await Image.LoadAsync(fileStream))
            {
                await image.SaveAsJpegAsync(FOLDER_PREFIX + filename, new JpegEncoder() { Quality = 75 });
            }
        }
    }

}
