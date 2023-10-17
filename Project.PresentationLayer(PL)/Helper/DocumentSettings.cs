using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Project.PresentationLayer_PL_.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file ,string folderName)
        {
            //var forlderpath = @"D:\back-end asp.net track (Route)\05 ASP Core MVC\MVC_MainProject\Project.PresentationLayer(PL)\wwwroot\Files\Imgs\";
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);
            var filename = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";
            var filepath = Path.Combine(folderpath, filename);
            using var filestream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(filestream);
            return filename;
        }
        public static void DeleteFile(string folderName, string filename )
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName,filename);
            if(File.Exists(filepath))
                File.Delete(filepath);
        }
        }
}
