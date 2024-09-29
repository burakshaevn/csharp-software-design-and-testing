using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Burakshaev.Lib.src.LogAn
{
    public static class ExtensionManagerFactory
    {
        private static IFileExtensionManager customManager = null;

        public static IFileExtensionManager Create()
        {
            if (customManager != null)
            {
                return customManager;
            }
            return new FileExtensionManager();
        }

        public static void SetManager(IFileExtensionManager manager)
        {
            customManager = manager;
        }
    }
}
