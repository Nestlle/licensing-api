namespace LicensingApi.Logging
{
    using System;
    using Microsoft.Extensions.Logging;
    using NReco.Logging.File;

    public class LoggingHandler
    {
        private string loggingPath = @"licensingApi1.log"; //TODO: put this into config file
        private LoggerFactory factory = new LoggerFactory();
        private static LoggingHandler _instance = null;
        public static LoggingHandler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoggingHandler();

                return _instance;
            }
        }

        private LoggingHandler()
        {
            factory.AddProvider(new FileLoggerProvider(loggingPath));
        }

        public void LogDebug(string message, Type type)
        {
            var logger = factory.CreateLogger("TEST");
            logger.LogDebug(message);
        }

        public void LogTrace(string message, Type type)
        {
            var logger = factory.CreateLogger("TEST");
            logger.LogTrace(message);
        }

        public void TestLog(string message, Type typee)
        {
             ILoggerFactory loggerFactory = new LoggerFactory(
                            new[] { new FileLoggerProvider(loggingPath) }
                        );
            ILogger logger = loggerFactory.CreateLogger<LoggingHandler>();
            logger.LogInformation("This is log message.");
        }

    }
}