using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bookmrqr.UserStub
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Enter search term");
                string searchTerm = Console.ReadLine();
                Console.WriteLine("search term: " + searchTerm);

                Console.WriteLine("Enter number of results");
                int numberOfResults;
                if (!int.TryParse(Console.ReadLine(), out numberOfResults))
                {
                    Console.WriteLine("Not a number.");
                    continue;
                }

                BingSearcher bingSearcher=new BingSearcher();
                List<Bookmark> result = bingSearcher.Search(searchTerm, numberOfResults).ToList();

                if (numberOfResults > result.Count)
                    numberOfResults = result.Count;

                foreach(Bookmark bookmark in result.Take(numberOfResults))
                {
                    Console.WriteLine(string.Format("Posting bookmark url={0}", bookmark.Url));

                    HttpClient client = new HttpClient();

                    client.BaseAddress = new Uri("http://localhost:5000/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Task<HttpResponseMessage> response = client.PostAsJsonAsync("api/accounts/test/bookmarks", bookmark);
                    //response.Wait();
                    //response.Result.EnsureSuccessStatusCode();                
                }
            }
            
        }
    }

    public class Bookmark
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
