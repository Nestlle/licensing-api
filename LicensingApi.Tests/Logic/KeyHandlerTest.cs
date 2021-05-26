using NUnit.Framework;
using LicensingApi.Logic;

namespace LicensingApi.Tests.Logic
{
    public class KeyHandlerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_GenerateKey_IsValid()
        {
            //Arange
            string license = KeyHandler.GenerateKey();
            //Act
            bool isKeyValid = KeyHandler.ValidateKey(license);
            //Assert
            Assert.IsTrue(isKeyValid);
        }
    }
}
