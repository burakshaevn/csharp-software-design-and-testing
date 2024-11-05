using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace KTPO4311.Burakshaev.Lib.src.LogAn
{
    public class FileExtensionManager : IFileExtensionManager
    {
        private string validExtension;

        public FileExtensionManager()
        {
            var path = @"C:\Users\burak\source\repos\KTPO4310.Burakshaev\KTPO4311.Burakshaev.Service\settings.json";
            var jsonString = File.ReadAllText(path);
            var settings = JsonSerializer.Deserialize<Settings>(jsonString);
            validExtension = settings.ValidExtension;
        }

        public bool IsValid(string fileName)
        {
            return fileName.EndsWith(validExtension);
        }
    }

    public class Settings
    {
        public string ValidExtension { get; set; }
    }
}
