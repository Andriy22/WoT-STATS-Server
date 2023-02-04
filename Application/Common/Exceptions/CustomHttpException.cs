using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class CustomHttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public CustomHttpException(string message, HttpStatusCode code = HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = code;
        }
    }
}
