using rss_reader.models;
using System.Xml.Linq;
using Xunit.Sdk;

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
        public static Feed? ReadFeed(string url)
        {
            try
            {
                XDocument doc = ParseFeed(url) ?? throw new ArgumentException("Feed has not been read correctly.");
                Feed newFeed = new(
                    doc.Descendants("title").First().Value,
                    doc.Descendants("description").First().Value,
                    doc.Descendants("category").First().Value,
                    url,
                    doc.Descendants("lastBuildDate").First().Value);

                int i = 0;

                foreach (XElement item in doc.Descendants("item"))
                {
                    Article newArticle = new(
                        item.Element("title")?.Value!, // if the doc is not null, a null title/link would be surprising
                        item.Element("link")?.Value!,
                        item.Element("pubDate")?.Value!,
                        item.Element("description")?.Value!
                        );
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
