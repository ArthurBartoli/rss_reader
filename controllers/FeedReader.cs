using rss_reader.models;
using rss_reader.toolbox;
using System.Xml.Linq;

namespace rss_reader.controllers
{
    /// <summary>
    /// Centralizes getting the xml feed from the url, parsing the xml to get feed info 
    /// and reading of the parsing output. 
    /// </summary>
    class FeedReader
    {
        /// <summary>
        /// Fetches the XML Feed from the url adress.
        /// </summary>
        /// <param name="url">Url to the feed XML</param>
        /// <remarks>This method needs to be called asynchronously or by awaiting results.</remarks>
        /// <returns>A string task</returns>
        private static async Task<string> GetFeedXml(string url)
        {
            using var client = new HttpClient(); // to send query and read response
            return await client.GetStringAsync(url); // await the http answer
        }
        /// <summary>
        /// Asynchronous version of the <see cref="ParseFeed"/> method because it awaits for the URL response
        /// of the <see cref="GetFeedXml(string)"/> method.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static async Task<XDocument?> ParseFeedAsync(string url)
        {
            try
            {
                var xml = await GetFeedXml(url);
                return XDocument.Parse(xml);
            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while parsing the feed.");
                Console.WriteLine(" !!!!!! " + e.Message);
                return null;
            }
        }
        /// <summary>
        /// Parses the XML object returned by the <see cref="GetFeedXml(string)"/> method.
        /// </summary>
        /// <param name="url">Url to the feed XML</param>
        /// <returns>An XDocument object containing the parsed XML feed.</returns>
        private static XDocument? ParseFeed(string url)
        {
            try
            {
                var xml = GetFeedXml(url).Result;
                return XDocument.Parse(xml);
            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while parsing the feed.");
                Console.WriteLine(" !!!!!! " + e.Message);
                return null;
            }
        }
        /// <summary>
        /// Asynchronous version of <see cref="ReadFeed"/> because it awaits for the URL response
        /// of the <see cref="GetFeedXml(string)"/> method.
        /// </summary>
        /// <param name="url">Url to the feed XML</param>
        /// <returns>A completed Feed object with a dict of completed Articles.</returns>
        public static async Task<Feed>? ReadFeedAsync(string url)
        {
            try
            {
                XDocument doc = await ParseFeedAsync(url) ?? throw new NullException("Feed has not been read correctly.");
                Feed newFeed = new(
                    doc.Descendants("title").First().Value,
                    doc.Descendants("description").First().Value,
                    doc.Descendants("category").First().Value,
                    doc.Descendants("lastBuildDate").First().Value,
                    url);

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
        /// <summary>
        /// Reads the XDocument object returned by the <see cref="ParseFeed(string)"/> method to get the 
        /// title, description, category and date of last build of this feed. The same is done for every
        /// article in the Feed corresponding to the url by reading the title, link, date of publication
        /// and description.
        /// All this information is stored into a Feed object containing a dict of Articles objects.
        /// </summary>
        /// <param name="url">Url to the feed XML</param>
        /// <returns>A completed Feed object with a dict of completed Articles.</returns>
        public static Feed? ReadFeed(string url)
        {
            try
            {
                XDocument doc = ParseFeed(url) ?? throw new NullException("Feed has not been read correctly.");
                Feed newFeed = new(
                    doc.Descendants("title").First().Value,
                    doc.Descendants("description").First().Value,
                    doc.Descendants("category").First().Value,
                    doc.Descendants("lastBuildDate").First().Value,
                    url);

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
