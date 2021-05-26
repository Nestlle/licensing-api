using NUnit.Framework;
using LicensingApi.Logic;
using FluentAssertions;
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
            string licenseKey;
            //Act
            licenseKey = KeyHandler.GenerateKey();
            var result = KeyHandler.ValidateKey(licenseKey);
            //Assert
            result.Should().BeTrue();
        }
    }
}
