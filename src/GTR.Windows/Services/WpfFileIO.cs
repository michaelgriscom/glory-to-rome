#region

using System;
using System.IO;

#endregion

namespace GTR.Windows.Services
{
    internal class WpfFileIo
    {
        private readonly string _appDataRoot = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public WpfFileIo()
        {
            if (!Directory.Exists(_appDataRoot))
            {
                Directory.CreateDirectory(_appDataRoot);
            }
        }

        public void CreateFile(string filename, string contents)
        {
            string filepath = GetPath(filename);
            File.WriteAllText(filepath, contents);
        }

        private string GetPath(string filename)
        {
            return Path.Combine(_appDataRoot, filename);
        }

        public bool FileExists(string filename)
        {
            string filepath = GetPath(filename);
            return File.Exists(filepath);
        }

        public bool DeleteFile(string filename)
        {
            string filepath = GetPath(filename);
            bool fileExists = FileExists(filepath);
            if (fileExists)
            {
                File.Delete(filepath);
            }
            return fileExists;
        }

        public string ReadFile(string filename)
        {
            string filepath = GetPath(filename);

            return File.ReadAllText(filepath);
        }
    }
}