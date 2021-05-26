using System;
using System.Collections.Generic;
using AppSoftware.LicenceEngine.Common;
using AppSoftware.LicenceEngine.KeyGenerator;
using AppSoftware.LicenceEngine.KeyVerification;
using LicensingApi.Logging;
using LicensingApi.Logic;
using LicensingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
/*
TODO: 
*/

namespace LicensingApi.Controllers
{
    [ApiController]
    [Route("license")]
    public class LicensingController : ControllerBase
    {
        private ILogger<LicensingController> _logger;
        public LicensingController(ILogger<LicensingController> _logger)
        {
            this._logger = (ILogger<LicensingController>)_logger;
        }
        [Route("generateLicense")]
        [HttpPost]
        public ActionResult GenerateLicense([FromQuery(Name = "us")] string userName, [FromQuery(Name = "app")] string application)
        {
            try
            {
                //LoggingHandler.Instance.TestLog("Test trace message", typeof(LicensingController));
                _logger.LogInformation($"New request comming with us:{userName}");
                UserLogic logic = new UserLogic(userName, application);
                User user = logic.GetUser();
                var licenseKey = GenerateKey();
                if (IsKeyValid(licenseKey))
                {
                    //TODO: save key into database after generation
                   // SaveKeyIntoDataBase(userName, application, licenseKey);
                }
            }
            catch (KeyNotFoundException e)
            {
                return StatusCode(400, e.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError($"There was a problem generating the license : {e.StackTrace}");
                return StatusCode(500, e.ToString());
            }
            return StatusCode(200);
        }

        private string GenerateKey()
        {
            //TODO: Store key bites somewhere or keep them here
            var keyByteSets = new[]
            {
                   new KeyByteSet(keyByteNumber: 1, keyByteA: 58, keyByteB: 6, keyByteC: 97),
                   new KeyByteSet(keyByteNumber: 2, keyByteA: 96, keyByteB: 254, keyByteC: 23),
                   new KeyByteSet(keyByteNumber: 3, keyByteA: 11, keyByteB: 185, keyByteC: 69),
                   new KeyByteSet(keyByteNumber: 4, keyByteA: 2, keyByteB: 93, keyByteC: 41),
                   new KeyByteSet(keyByteNumber: 5, keyByteA: 62, keyByteB: 4, keyByteC: 234),
                   new KeyByteSet(keyByteNumber: 6, keyByteA: 200, keyByteB: 56, keyByteC: 49),
                   new KeyByteSet(keyByteNumber: 7, keyByteA: 89, keyByteB: 45, keyByteC: 142),
                   new KeyByteSet(keyByteNumber: 8, keyByteA: 6, keyByteB: 88, keyByteC: 32)
            };

            int seed = new Random().Next(0, int.MaxValue); // generated random seed. Seed can be user id / number of users / etc.
            var pkvKeyGenerator = new PkvKeyGenerator();
            string licenceKey = pkvKeyGenerator.MakeKey(seed, keyByteSets);

            return licenceKey;
        }
        private void SaveKeyIntoDataBase(string userName, string application, object licenseKey)
        {
            throw new NotImplementedException();
        }

        private bool IsKeyValid(string licenseKey)
        {
            //TODO: Store key bites somewhere or keep them here
            var keyByteSets = new[]
            {
                    new KeyByteSet(keyByteNumber: 1, keyByteA: 58, keyByteB: 6, keyByteC: 97),
                    new KeyByteSet(keyByteNumber: 5, keyByteA: 62, keyByteB: 4, keyByteC: 234),
                    new KeyByteSet(keyByteNumber: 8, keyByteA: 6, keyByteB: 88, keyByteC: 32)
            };

            // Enter the key generated in the SampleKeyGenerator console app

            var pkvKeyVerifier = new PkvKeyVerifier();

            PkvKeyVerificationResult pkvKeyVerificationResult = pkvKeyVerifier.VerifyKey(

                key: licenseKey?.Trim(),
                keyByteSetsToVerify: keyByteSets,
                totalKeyByteSets: 8,
                blackListedSeeds: null
            );

            if (pkvKeyVerificationResult == PkvKeyVerificationResult.KeyIsValid)
            {
                return true;
            }
            return false;

        }

        public ActionResult ValidateLicense(string username, string application, string licenseKey)
        {
            return StatusCode(200);
        }
    }
}