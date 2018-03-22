using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights;

namespace DemoApplication.ErrorHandler
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private readonly TelemetryClient _telemetryClient;

        public GlobalExceptionLogger()
        {
            _telemetryClient = new TelemetryClient();
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _telemetryClient.TrackTrace(context.Exception.StackTrace);
        }
    }
}