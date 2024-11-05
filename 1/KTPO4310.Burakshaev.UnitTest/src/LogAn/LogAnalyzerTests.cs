using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTPO4311.Elmashad.Lib.src.LogAn;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace KTPO4311.Elmashad.UnitTest.src.LogAn
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidLogFileName_BadExtension_ResunsFalse()
        {
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            bool result = logAnalyzer.IsValidFileName("filewithbadextension.foo");
            Assert.That(result, Is.False);
        }
        [TestCase("filewithbadextension.ELMASHAD")]
        [TestCase("filewithbadextension.elmashad")]
        public void IsValidLogFileName_GoodExtension_ReturnsTrue(string file)
        {
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            bool result = logAnalyzer.IsValidFileName(file);
            Assert.That(result, Is.True);
        }
        //[Test]
        //public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        //{
        //    LogAnalyzer logAnalyzer = new LogAnalyzer();
        //    bool result = logAnalyzer.IsValidFileName("ELMASHAD");
        //    Assert.That(result, Is.True);
        //}
        //[Test]
        //public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        //{
        //    LogAnalyzer logAnalyzer = new LogAnalyzer();
        //    bool result = logAnalyzer.IsValidFileName("elmashad");
        //    Assert.That(result, Is.True);
        //}

        [Test]
        public void IsValidFileName_EmptyFileName_Throws()
        {
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            var ex = Assert.Catch<Exception>(() => logAnalyzer.IsValidFileName(""));
            StringAssert.Contains("Некорректный файл", ex.Message);
        }

        [TestCase("badfile.foo", false)]
        [TestCase("goodfile.elmashad", true)]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected) {
            var logAnalyzer = new LogAnalyzer();
            logAnalyzer.IsValidFileName(file);
            NUnit.Framework.Legacy.ClassicAssert.AreEqual(expected, logAnalyzer.WasLastFineNameValid);
        }
    }
}
