using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Models;
using Views;

namespace Controllers{
    class RssReaderController
    {
        private static async Task<String> GetFeedXml(string url)
        {
            try
            {
                using var client = new HttpClient(); // to send query and read response
            //  ÎÎÎÎÎ to free the instantiated object no matter the exit code (forces Dispose() method)
                return await client.GetStringAsync(url); // await the http answer
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while parsing the feed.");
                Console.WriteLine(e.Message);
                return null;
            }
        }
        private static XDocument ParseFeed(string url)
        {   
            string xml = GetFeedXml(url).Result;
            return XDocument.Parse(xml);
        }
        public static Feed LoadFeed(string url) // Async method to free thread at await
        {   
            try
            {
                XDocument doc = (XDocument) ParseFeed(url);
                Feed newFeed = new Feed();

                newFeed.Title = doc.Descendants("title").First().Value;
                newFeed.Description = doc.Descendants("description").First().Value;
                newFeed.Category = doc.Descendants("category").First().Value;

                foreach (XElement item in doc.Descendants("item"))
                {
                    Article newArticle = new Article();
                    newArticle.Title = item.Element("title")?.Value;
                    newArticle.Link = item.Element("link")?.Value;
                    newArticle.Date = item.Element("pubDate")?.Value;
                    newArticle.Description = item.Element("description")?.Value;

                    newFeed.Articles.Add(newArticle);
                }

                return newFeed;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while loading feed.");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static void DisplayFeed(Feed feed)
        {
            ConsoleView viewer = new ConsoleView();    
            try
            {
                foreach (Article item in feed.Articles)
                {
                    viewer.DisplayArticle(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while displaying feed");
                Console.WriteLine(e.Message);
            }
        }
    }
}