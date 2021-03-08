using System;
using System.IO;
using Diploma.Interfaces;
using Interfaces;

namespace Diploma.FileManager
{
    public class FileManager: IInitialization, IFileManager
    {
        private readonly string _storage;
        
        public FileManager()
        {
            var platform = Environment.OSVersion.Platform;
            var homePath = (platform == PlatformID.Unix || platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            
            _storage = Path.Combine(Path.Combine(homePath, "Documents"), "MDGameStorage");
            Directory.CreateDirectory(_storage);
        }

        /// <inheritdoc />
        public bool FileExists(int FileId, string fileName)
        {
            var filePath = Path.Combine(_storage, FileId.ToString(), fileName);
            return File.Exists(filePath);
        }


        /// <inheritdoc />
        public string CreateFileFolder(string FileId)
        {
            var Folder = Path.Combine(_storage, FileId);
            Directory.CreateDirectory(Folder);
            return Folder;
        }

        /// <inheritdoc />
        public void DeleteFolder(int guideId)
        {
            var folderPath = Path.Combine(_storage, guideId.ToString());
            Directory.Delete(folderPath, true);
        }

        public void Initialization() { }
    }
}