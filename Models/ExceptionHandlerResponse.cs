namespace CashierApi.Models
{
    public class ExceptionHandlerResponse
    {
        public int StatusCode { get; set; } 

        public string Message;

        public string? Details;

        public ExceptionHandlerResponse(int statusCode,string message,string? details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
            
        }
    }
}
