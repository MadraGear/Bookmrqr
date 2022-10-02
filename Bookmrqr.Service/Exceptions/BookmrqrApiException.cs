using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quintor.Bookmrqr.Service.Exceptions
{
    public class BookmrqrApiException : Exception
    {
        public BookmrqrApiException()
        {
        }

        public BookmrqrApiException(string message):base(message)
        {
        }
        
        public BookmrqrApiException(string message, Exception innerException): base(message, innerException)
        {
        }

    }
}