namespace Hahn.ApplicationProcess.May2020.Domain.Constants
{
    public class AppConstants
    {
        public class ErrorMessages
        {
            public const string GenericError = "Something bad happened. Please try again later. If problem persists, please contact support team.";
            public const string InvalidCountry = "Must be a valid country name";
            public const string StartupFailed = "Application start-up failed";
        }

        public class InfoMessages
        {
            public const string StartingUp = "Starting up";
        }

        public class ContentTypes
        {
            public const string Json = "application/json";
        }
    }
}
