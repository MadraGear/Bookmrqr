using System.Collections.Generic;
using System.Linq;
using Bookmrqr.Viewer.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookmrqr.Viewer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public List<Bookmark> Bookmarks { get; set; }

        public int TotalBookmarks { get; set; }

        public int UnProcessedBookmarks { get; set; }

        public int ProcessedBookmarks { get; set; }

        public IndexModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void OnGet()
        {
            Dictionary<bool, int> processedData = _appDbContext.Bookmarks
                .GroupBy(b => b.IsProcessed)
                .ToDictionary(grp => grp.Key, grp => grp.Count());


            ProcessedBookmarks = processedData.ContainsKey(true)
                ? processedData[true]
                : 0;
            
            UnProcessedBookmarks = processedData.ContainsKey(false)
                ? processedData[false]
                : 0;

            TotalBookmarks = processedData.Values.Sum();

            Bookmarks = _appDbContext.Bookmarks
                .OrderByDescending(b => b.Timestamp)
                .Take(20)
                .ToList();
        }
    }
}