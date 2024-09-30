using KTPO4311.Burakshaev.Lib.src.LogAn;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTPO4311.Burakshaev.UnitTest.src.LogAn
{
    internal class FakeExtensionManager : IFileExtensionManager
    {
        public bool WillBeWalid = false;
        public Exception WillThrow = null;
        public bool IsValid(string filename)
        {
            if (WillThrow != null)
            {
                throw WillThrow;
            }
            return WillBeWalid;
        }

        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            FakeExtensionManager extensionManager = new FakeExtensionManager();
            extensionManager.WillBeWalid = true;
            ExtensionManagerFactory.SetManager(extensionManager);
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            Assert.That(logAnalyzer.IsValidLogFileName("short.ext"), Is.True);
        }

        [Test]
        public void IsValidFileName_NameUnSupportedExtension_ReturnsFalse()
        {
            FakeExtensionManager extensionManager = new FakeExtensionManager();
            extensionManager.WillBeWalid = false;
            ExtensionManagerFactory.SetManager(extensionManager);
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            Assert.That(logAnalyzer.IsValidLogFileName("short.ext"), Is.False);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            FakeExtensionManager extensionManager = new FakeExtensionManager();
            extensionManager.WillThrow = new InvalidOperationException("Test Exception");
            ExtensionManagerFactory.SetManager(extensionManager);
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            Assert.That(logAnalyzer.IsValidLogFileName("short.ext"), Is.False);
        }
    }
}
