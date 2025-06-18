using Microsoft.AspNetCore.Mvc;
using Zadatak1.Services;
namespace Zadatak1.Controllers
{
    public class BaseController : Controller
    {
        public readonly ExceptionHandler _exceptionHandler;
        public BaseController(ExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        protected async Task<IActionResult> TryExecute<T>(Func<Task<T>> func, string errorMessageId = null)
        {
            try
            {
                var result = await func();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var handled = _exceptionHandler.HandleException<T>(ex, errorMessageId);
                return StatusCode((int)handled.ResultStatus, handled.ErrorMessage);
            }
        }
        protected async Task<(bool Success, T Result)> TryExecuteWithResult<T>(Func<Task<T>> func, string errorMessageId = null)
        {
            try
            {
                var result = await func();
                return (true, result);
            }
            catch (Exception ex)
            {
                _exceptionHandler.HandleException<T>(ex, errorMessageId); 
                return (false, default);
            }
        }


    }
}
