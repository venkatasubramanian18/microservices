using System.Net;

namespace PE.EmployeeAPIService.Models
{
    internal class Error
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
            
    }
}
