using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KTPO4311.Burakshaev.Lib.src.LogAn;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace KTPO4311.Burakshaev.UnitTest.src.LogAn
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            FakeWebService mockWebService = new FakeWebService();
            WebServiceFactory.SetService(mockWebService);
            LogAnalyzer log = new LogAnalyzer();
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            StringAssert.Contains("Слишком короткое имя файла: " + tooShortFileName, mockWebService.last_error);
        }
        [TearDown]
        public void AfterEachTest()
        {
            ExtensionManagerFactory.SetManager(null);
            WebServiceFactory.SetService(null);
            EmailServiceFactory.SetEmail(null);
        }
        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            FakeWebService studWebService = new FakeWebService(); 
            WebServiceFactory.SetService(studWebService);
            studWebService.exception_message = new Exception("Это подделка.");

            FakeEmailService mockEmail = new FakeEmailService();
            EmailServiceFactory.SetEmail(mockEmail);

            LogAnalyzer logAnalyzer = new LogAnalyzer();
            string tooShortFileName = "abc.ext";
            
            logAnalyzer.Analyze(tooShortFileName);

            StringAssert.Contains("some@email.com", mockEmail.To);
            StringAssert.Contains("Это подделка", mockEmail.Body);
            StringAssert.Contains("Невозможно открыть веб-сервис", mockEmail.Subject);
        }

        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = true;
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("short.ext");
            Assert.That(result, Is.True);
        }
        [Test]
        public void IsValidFileName_NoneSupportedExtension_ReturnsFalse()
        {
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillBeValid = false;
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("short.ext");
            Assert.That(result, Is.False);
        }
        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            FakeExtensionManager fakeManager = new FakeExtensionManager();
            fakeManager.WillThrow = new Exception("Some exception");
            ExtensionManagerFactory.SetManager(fakeManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("short.ext");
            Assert.That(result, Is.False);
        }
    }
}
