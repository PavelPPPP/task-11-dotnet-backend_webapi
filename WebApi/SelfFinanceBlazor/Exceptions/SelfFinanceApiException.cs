using System.Net;

namespace SelfFinanceBlazor.Exceptions
{
    public class SelfFinanceApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public SelfFinanceApiException(string message, HttpStatusCode statusCode)
           : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
