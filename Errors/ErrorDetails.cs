using System.Net;

namespace WebApplication1.Errors
{
    public class ErrorDetails
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
