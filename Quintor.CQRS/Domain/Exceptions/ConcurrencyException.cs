using System;

namespace Quintor.CQRS.Domain.Exceptions
{
    public class ConcurrencyException : System.Exception
    {
        public ConcurrencyException(string id)
            : base(string.Format("A different version than expected was found in aggregate {0}", id))
        {
        }
    }
}
