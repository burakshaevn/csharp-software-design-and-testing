using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTPO4311.Burakshaev.Lib.src.LogAn;

namespace KTPO4311.Burakshaev.UnitTest.src.LogAn
{
    [TestFixture]
    public class LogAnalyzerNSubstituteTests
    {
        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            IFileExtensionManager fakeExtensionManager = Substitute.For<IFileExtensionManager>();
            fakeExtensionManager.IsValid("filename.ext").Returns(true);
            ExtensionManagerFactory.SetManager(fakeExtensionManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("filename.ext");
            Assert.That(result, Is.True);
        }
        [Test]
        public void IsValidFileName_NoneSupportedExtension_ReturnsFalse()
        {
            IFileExtensionManager fakeExtensionManager = Substitute.For<IFileExtensionManager>();
            fakeExtensionManager.IsValid("filename.ext").Returns(false);
            ExtensionManagerFactory.SetManager(fakeExtensionManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("example.unsupported");
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            IFileExtensionManager fakeExtensionManager = Substitute.For<IFileExtensionManager>();
            fakeExtensionManager.When(x => x.IsValid(Arg.Any<string>()))
                .Do(content => { throw new Exception("Some exception"); });
            ExtensionManagerFactory.SetManager(fakeExtensionManager);
            LogAnalyzer log = new LogAnalyzer();
            bool result = log.IsValidLogFileName("filename.ext");
            Assert.That(result, Is.False);
        }

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            IWebService studWebService = Substitute.For<IWebService>();
            studWebService.When(x => x.LogError(Arg.Any<string>())).Do(x => { throw new Exception("Это подделка"); });
            WebServiceFactory.SetService(studWebService);
            
            IEmailService mockEmail = Substitute.For<IEmailService>();
            EmailServiceFactory.SetEmail(mockEmail);

            string tooShortFileName = ".txt";

            LogAnalyzer log = new LogAnalyzer();
            log.Analyze(tooShortFileName);
            
            mockEmail.Received().SendEmail(
                Arg.Is<string>(s => s == "some@email.com"),
                Arg.Is<string>(s => s.Contains("Невозможно открыть веб-сервис")),
                Arg.Is<string>(s => s.Contains("Это подделка"))
                );
        }

        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            IWebService webService = Substitute.For<IWebService>();
            WebServiceFactory.SetService(webService);
            
            string tooShortFileName = ".txt";
            
            LogAnalyzer log = new LogAnalyzer();
            log.Analyze(tooShortFileName);
            webService.Received().LogError("Слишком короткое имя файла: " + tooShortFileName);
        }
    }
}
