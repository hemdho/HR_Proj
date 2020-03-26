using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HR.WebApi.Interfaces
{
    public interface IDocuments
    {
        string GenerateFileName(string fileName);

        string UploadFile(IList<IFormFile> files, string folderPath);

        string DownloadFile(string fileName);

        bool DeleteFile(string folderName,string fileName);

        bool BackupFile(string fileName,string folderName);

        string GetPath(string folderName);

        string GetBackupPath(string folderName);

        bool isFileExists(string fileName);
    }
}
