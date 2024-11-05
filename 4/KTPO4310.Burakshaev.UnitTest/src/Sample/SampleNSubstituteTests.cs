using KTPO4311.Burakshaev.Lib.src.LogAn;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTPO4311.Burakshaev.UnitTest.src.Sample
{
    [TestFixture]
    public class SampleNSubstituteTests
    {
        [Test]
        public void Returns_ParticularArg_Works()
        {
            IFileExtensionManager fileExtensionManager = Substitute.For<IFileExtensionManager>(); 
            fileExtensionManager.IsValid("validfile.ext").Returns(true);
            bool result = fileExtensionManager.IsValid("validfile.ext");
            Assert.That(result, Is.True);
            // assert(result == true);
        }

        [Test]
        public void Returns_ArgAny_Works()
        {
            IFileExtensionManager fakeExtensionManager = Substitute.For<IFileExtensionManager>();
            fakeExtensionManager.IsValid(Arg.Any<string>()).Returns(true);
            bool result = fakeExtensionManager.IsValid("anyfile.ext");
            Assert.That(result, Is.True); 
        }

        [Test]
        public void Returns_ArgAny_Throws()
        {
            IFileExtensionManager fakeExtensionManager = Substitute.For<IFileExtensionManager>();
            fakeExtensionManager.When(x => x.IsValid(Arg.Any<string>()))
                .Do(content => { throw new Exception("fake exception"); });
            Assert.Throws<Exception>(() => fakeExtensionManager.IsValid("anything"));
        }

        [Test]  
        public void Received_ParticularArg_Saves()
        {
            IWebService mockWebService = Substitute.For<IWebService>();
            mockWebService.LogError("Поддельное сообщение");
            mockWebService.Received().LogError("Поддельное сообщение");
        }
    }
}
