using Zadatak1.Models;

namespace Zadatak1.Services
{
    public class ExceptionHandlerService
    {
        private readonly ILogger<ExceptionHandlerService> _logger;

        public ExceptionHandlerService(ILogger<ExceptionHandlerService> logger)
        {
            _logger = logger;
        }

        public ActionResult<T> HandleException<T>(Exception exception, string errorMessageId = null)
        {
            var result = new ActionResult<T>();

            switch (exception)
            {
                case UnauthorizedAccessException:
                    result.ResultStatus = ActionResultStatus.Unauthorized;
                    result.ErrorMessage = errorMessageId ?? "Unauthorized";
                    break;

                case ArgumentException:
                    result.ResultStatus = ActionResultStatus.BadRequest;
                    result.ErrorMessage = errorMessageId ?? "Invalid argument.";
                    break;

                default:
                    result.ResultStatus = ActionResultStatus.ServerError;
                    result.ErrorMessage = errorMessageId ?? "Server error occurred.";
                    break;
            }

            result.ErrorData = exception.Message;
            _logger.LogError(exception, exception.ToString());

            return result;
        }
    }
}
