using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Elmashad.Lib.src.LogAn
{
    public  class LogAnalyzer
    {
        public bool WasLastFineNameValid { get; set; }
        public bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Некорректный файл.");
            }
            if (fileName.EndsWith("ELMASHAD", StringComparison.CurrentCultureIgnoreCase))
            {
                WasLastFineNameValid = true;
                return true;
            }
            WasLastFineNameValid = false;
            return false;
        }
    }
}
