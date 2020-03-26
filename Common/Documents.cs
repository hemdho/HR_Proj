using HR.WebApi.DAL;
using HR.WebApi.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HR.WebApi.Common
{
    public class Documents : IDocuments
    {
        //private readonly string DocumentPath = AppSettings.DocumentPath;
        //private readonly string DocumentPathBackup = AppSettings.DocumentPathBackup;
        private readonly string DocumentPath = @"D:\BnSGroup\";
        private readonly string DocumentPathBackup = @"D:\BnSGroup\Backup\";
        public Documents()
        {
        }

        public bool BackupFile(string fileName,string folderName)
        {
            bool blnBackUpFile = false;
            try
            {
                string[] files = Directory.GetFiles(GetPath(folderName), fileName);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    var saveBackUpFile = GetBackupPath(folderName);

                    //if folder not exist then Create folder
                    if (!Directory.Exists(saveBackUpFile))
                        Directory.CreateDirectory(saveBackUpFile);

                    // Use static Path methods to extract only the file name from the path.
                    fileName = Path.GetFileName(s);
                    fileName = GenerateFileName(fileName);
                    saveBackUpFile = Path.Combine(saveBackUpFile, fileName);
                    File.Copy(s, saveBackUpFile, true);
                }
                blnBackUpFile = true;
            }
            catch (Exception ex)
            {
                blnBackUpFile = false;
                throw ex;
            }
            return blnBackUpFile;
        }

        public bool DeleteFile(string folderName,string fileName)
        {
            bool blnDeleteFile = false;
            try
            {
               var pathToSave = GetPath(folderName);
                var fullPath = Path.Combine(pathToSave, fileName);
                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                blnDeleteFile = false;
                throw ex;
            }
            return blnDeleteFile;            
        }

        public string DownloadFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public string GenerateFileName(string fileName)
        {
            try
            {
                string strFileName = string.Empty;
                string[] strName = fileName.Split('.');
                strFileName = strName[0] + "_" + DateTime.Now.ToString("yyyyMMdd") + "." + strName[strName.Length - 1];
                return strFileName;
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }

        public string GetBackupPath(string folderName)
        {
            try
            {
                return Path.Combine(DocumentPathBackup, folderName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public string GetPath(string folderName)
        {
            try
            {
                return Path.Combine(DocumentPath, folderName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public bool isFileExists(string fileName)
        {
            try
            {
                bool blnFileExists = false;
                if (File.Exists(fileName))
                    blnFileExists = true;
                return blnFileExists;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public string UploadFile(IList<IFormFile> files, string folderPath)
        {
            string StrValue = string.Empty;
            try
            {
                var pathToSave = GetPath(folderPath);

                //if folder not exist then Create folder
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = file.FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);

                        if (isFileExists(fullPath))
                            BackupFile(fileName, folderPath);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        StrValue = fullPath;
                    }
                }
            }
            catch (Exception ex)
            {
                StrValue = "";
                throw ex;
            }
            return StrValue;
        }
    }
}
