using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bookmrqr.UserStub
{
    public class BingSearcher
    {
        const string accessKey = "9eb01751004d4ff7b0e68437836f6e17";
        const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";

        public IEnumerable<Bookmark> Search(string searchTerm, int numberOfResults)
        {
            int maxOffset = numberOfResults/10;
            if (maxOffset < 1)
                maxOffset = 1;

            for (int offset=0;offset < maxOffset;offset++)
            {
                string uriQuery = uriBase 
                    + "?q=" + Uri.EscapeDataString(searchTerm)
                    + "&count=10&offset=" + offset;

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", accessKey);
                var httpResponseMessage = client.GetAsync(uriQuery).Result;
                var json = httpResponseMessage.Content.ReadAsStringAsync().Result;

                foreach(var bookmark in  GetUrls(json))
                {
                    yield return bookmark;
                }
            }
        }

        static IEnumerable<Bookmark> GetUrls(string json)
        {
            JObject obj = JObject.Parse(json);
            foreach (JToken token in obj.FindTokens("value"))
            {
                foreach (var value in token.Children())
                {
                    JToken name = value.SelectToken("name");
                    JToken url = value.SelectToken("url");
                    if (name != null && url != null)
                    {
                        yield return new Bookmark
                        {
                            Id = Guid.NewGuid().ToString(),
                            Url = url.ToString(),
                            Title = name.ToString(),
                            CreatedAt = DateTime.Now
                        };
                    }
                }
            }
        }
    }

}