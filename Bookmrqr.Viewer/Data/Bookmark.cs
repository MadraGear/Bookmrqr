using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmrqr.Viewer.Data
{
    public class Bookmark
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string UserName { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsProcessed { get; set; }
    }
}
