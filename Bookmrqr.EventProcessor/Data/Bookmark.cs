using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmrqr.EventProcessor.Data
{
    public class Bookmark
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string UserName { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
    }
}
