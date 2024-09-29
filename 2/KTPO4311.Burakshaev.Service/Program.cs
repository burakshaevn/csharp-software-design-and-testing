using KTPO4311.Burakshaev.Lib.src.LogAn;
using System;

namespace KTPO4310.Ivanov.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            string[] fileNames = { "file.log", "file.txt", "report.log", "data.docx" };

            foreach (var fileName in fileNames)
            {
                bool result = analyzer.IsValidLogFileName(fileName);
                Console.WriteLine($"{fileName}: {result}");
            }
        }
    }
}
