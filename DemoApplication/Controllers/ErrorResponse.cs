namespace DemoApplication.Controllers
{
    public class ErrorResponse
    {
        public ErrorResponse(string errorMessage, string parameterName)
        {
            ErrorMessage = errorMessage;
            ParameterName = parameterName;
        }

        public string ErrorMessage { get; }

        public string ParameterName { get; }
    }
}