using System;
using System.IO;
using Diploma.Interfaces;
using Interfaces;

namespace Diploma.Managers
{
    public class FileManager: IFileManager
    {
        private readonly string _storage;
        
        public FileManager()
        {
            // var platform = Environment.OSVersion.Platform;
            // var homePath = (platform == PlatformID.Unix || platform == PlatformID.MacOSX)
            //     ? Environment.GetEnvironmentVariable("HOME")
            //     : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            //
            _storage = AppDomain.CurrentDomain.BaseDirectory;
            //CreateFileFolder("LocalDataStorage");
            var directoryInfo = new DirectoryInfo(_storage);
            _storage = directoryInfo.GetDirectories()[0].ToString();
            _storage = Path.Combine(_storage, "StreamingAssets");
            _storage = CreateFileFolder("LocalDataStorage");
        }

        /// <inheritdoc />
        public bool FileExists(int FileId, string fileName)
        {
            var filePath = Path.Combine(_storage, FileId.ToString(), fileName);
            return File.Exists(filePath);
        }

        public string GetStorage()
        {
            return _storage;
        }

        /// <inheritdoc />
        public string CreateFileFolder(string FileId)
        {
            var Folder = Path.Combine(_storage, FileId);
            Directory.CreateDirectory(Folder);
            return Folder;
        }

        /// <inheritdoc />
        public void DeleteFolder(string folder)
        {
            Directory.Delete(folder, true);
        }
    }
}