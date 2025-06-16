namespace Zadatak1.Models
{
    public enum ActionResultStatus
    {
        Success,
        BadRequest,
        Unauthorized,
        ServerError,
        NotFound
    }

    public class ActionResult<T>
    {
        public ActionResultStatus ResultStatus { get; set; }
        public string ErrorMessage { get; set; }
        public object ErrorData { get; set; }
        public T Data { get; set; }
    }
}
