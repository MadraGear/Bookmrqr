using System.Collections.Generic;

namespace Quintor.Bookmrqr.Service.Models
{
    public class Bookmarks
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int PageNumber { get; set; }
        public List<Bookmark> Items { get; set; }
    }
}