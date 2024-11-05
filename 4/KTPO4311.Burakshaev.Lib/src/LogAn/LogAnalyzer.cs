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
        public void Analyze(string fileName)
        {
            try
            {
                if (fileName.Length < 8)
                {
                    IWebService webService = WebServiceFactory.Create();
                    webService.LogError("Слишком короткое имя файла: " + fileName);
                }
            }
            catch (Exception ex)
            {
                IEmailService emailService = EmailServiceFactory.Create();
                emailService.SendEmail("some@email.com", "Невозможно открыть веб-сервис", ex.Message);
            }
        }
    }
}
