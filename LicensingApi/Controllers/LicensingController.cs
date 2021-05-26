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
                var licenseKey = KeyHandler.GenerateKey();
                if (KeyHandler.ValidateKey(licenseKey))
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

        public ActionResult ValidateLicense(string username, string application, string licenseKey)
        {
            return StatusCode(200);
        }
    }
}