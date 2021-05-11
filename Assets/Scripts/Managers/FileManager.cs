using System;
using System.IO;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;

namespace Diploma.Managers
{
    public class FileManager: IFileManager
    {
        private readonly string _storage;
        
        public FileManager()
        {
            _storage = AppDomain.CurrentDomain.BaseDirectory;
            var directoryInfo = new DirectoryInfo(_storage);
            #if UNITY_EDITOR
            _storage = directoryInfo.GetDirectories()[0].ToString();
            #else
            _storage = directoryInfo.GetDirectories()[0].GetDirectories()[3].ToString();
            #endif
            _storage = CreateFileFolder("LocalDataStorage");
        }
        
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