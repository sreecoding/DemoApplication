using System.Web.Http.ExceptionHandling;

namespace DemoApplication.ErrorHandler
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var telemetry = new Microsoft.ApplicationInsights.TelemetryClient();
            telemetry.TrackTrace("Error Occured");
        }
    }
}