using rss_reader.models;
using System.Xml.Linq;

namespace rss_reader.controllers
{
    class FeedReader
    {
        private static async Task<string> GetFeedXml(string url)
        {
            using var client = new HttpClient(); // to send query and read response
            return await client.GetStringAsync(url); // await the http answer
        }
        private static XDocument? ParseFeed(string url)
        {
            try
            {
                string xml = GetFeedXml(url).Result;
                return XDocument.Parse(xml);
            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while parsing the feed.");
                Console.WriteLine(" !!!!!! " + e.Message);
                return null;
            }
        }
        public static Feed ReadFeed(string url)
        {
            try
            {
                XDocument doc = ParseFeed(url);
                Feed newFeed = new Feed();

                newFeed.Title = doc.Descendants("title").First().Value;
                newFeed.Description = doc.Descendants("description").First().Value;
                newFeed.Category = doc.Descendants("category").First().Value;
                newFeed.Link = url;
                newFeed.LastBuildDate = doc.Descendants("lastBuildDate").First().Value;

                int i = 0;

                foreach (XElement item in doc.Descendants("item"))
                {
                    Article newArticle = new Article();

                    newArticle.Title = item.Element("title")?.Value;
                    newArticle.Link = item.Element("link")?.Value;
                    newArticle.PubDate = item.Element("pubDate")?.Value;
                    newArticle.Description = item.Element("description")?.Value;

                    newFeed.Articles[i.ToString()] = newArticle;
                    i++;
                }

                return newFeed;
            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while loading feed.");
                Console.WriteLine(" !!!!!! " + e.Message);
                return null;
            }
        }
    }
}
