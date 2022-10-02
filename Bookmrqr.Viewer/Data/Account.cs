using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmrqr.Viewer.Data
{
    public class Account
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2} - {3}", Id, Version, DisplayName, Email);
        }
    }
}
