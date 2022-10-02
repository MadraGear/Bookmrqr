using System;

namespace Quintor.Bookmrqr.Service.Models
{
    public class Bookmark
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}