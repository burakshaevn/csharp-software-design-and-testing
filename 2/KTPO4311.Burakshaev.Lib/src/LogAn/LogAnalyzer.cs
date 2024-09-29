using System;

namespace KTPO4311.Burakshaev.Lib.src.LogAn
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
            IFileExtensionManager fileExtensionManager = ExtensionManagerFactory.Create();
            try {
                return fileExtensionManager.IsValid(fileName);
            }
            catch {
                return false;
            }
        }
        
    }
}
