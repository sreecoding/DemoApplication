namespace DemoApplication.Controllers
{
    public class ErrorResponse
    {
        public ErrorResponse(string parameterName,string errorMessage)
        {
            ErrorMessage = errorMessage;
            ParameterName = parameterName;
        }

        public string ErrorMessage { get; }

        public string ParameterName { get; }
    }
}