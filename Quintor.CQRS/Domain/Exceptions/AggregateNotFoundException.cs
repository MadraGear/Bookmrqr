using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quintor.CQRS.Domain.Exceptions
{
    public class AggregateNotFoundException : System.Exception
    {
        public AggregateNotFoundException(string id)
            : base(string.Format("Aggregate {0} was not found", id))
        {
        }
    }
}
