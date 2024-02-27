using rss_reader.controllers;
using rss_reader.toolbox;
using System.Text;

namespace rss_reader.models
{
    /// <summary>
    /// Core of the backend, collects all feed loaded as a dictionary and 
    /// handles adding feed, and import/export of a list of feeds.
    /// </summary>
    public class FeedList
    {
        public Dictionary<string, Feed> Feeds { get; set; }

        public FeedList()
        {
            Feeds = new Dictionary<string, Feed>();
        }
        /// <summary>
        /// Asynchronous version <see cref="AddFeed(string)"/>
        /// </summary>
        /// <param name="url">Url to the feed XML</param>
        public async Task AddFeedAsync(string url)
        {
            Feed newFeed = await FeedReader.ReadFeedAsync(url)
                ?? throw new NullException("Feed is null, something wrong happened");
            if (Feeds.Count == 0)
            {
                Feeds["0"] = newFeed;
                return;
            }

            int maxIndex = this.Feeds.Keys
                .Select(key => int.Parse(key))
                .Max();

            Feeds[(maxIndex + 1).ToString()] = newFeed;
        }
        /// <summary>
        /// Gets a <see cref="Feed"/> from the <see cref="FeedReader"/> object and
        /// adds it to <see cref="Feeds"/>.
        /// </summary>
        /// <param name="url">Url to the feed XML</param>
        public void AddFeed(string url)
        {
            Feed newFeed = FeedReader.ReadFeed(url)
                ?? throw new NullException("Feed is null, something wrong happened");
            if (Feeds.Count == 0)
            {
                Feeds["0"] = newFeed;
                return;
            }

            int maxIndex = this.Feeds.Keys
                .Select(key => int.Parse(key))
                .Max();

            Feeds[(maxIndex + 1).ToString()] = newFeed;
        }

        /// <summary>
        /// Exports the current selection of <see cref="Feeds"/> as a textfile.
        /// </summary>
        /// <param name="path">Path to the export, including the name of the file.</param>
        /// <example>ExportList("C:\Users\user\rss_reader\export\export.txt")</example>
        public void ExportList(string path)
        {
            try
            {
                if (Feeds.Count == 0)
                {
                    Console.WriteLine("No feed has been loaded.");
                    return;
                }

                using StreamWriter F = new(path);
                foreach (string item in Feeds.Keys)
                {
                    F.WriteLine("####################");
                    F.WriteLine(Feeds[item].Title + "||");
                    F.WriteLine(Feeds[item].Description + "||");
                    F.WriteLine(Feeds[item].LastBuildDate + "||");
                    F.WriteLine(Feeds[item].Link);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while importing Feed List");
                Console.WriteLine(" !!!!!! " + e.Message);
            }
        }

        /// <summary>
        /// Asynchronous version of <see cref="ImportList(string)"/>.
        /// This is needed because the XML of each field is queried again to get the 
        /// articles' information.
        /// </summary>
        /// <param name="path">Path to the export, including the name of the file.</param>
        /// <example>ImportList("C:\Users\user\rss_reader\export\export.txt")</example>
        public async Task ImportListAsync(string path)
        {
            try
            {
                StringBuilder importBuilder = new();
                using (StreamReader F = new(path))
                {
                    // We jump the first line to remove the first separator which separates nothing
                    F.ReadLine();

                    string line;
                    while ((line = F.ReadLine()!) != null) { importBuilder.Append(line); }
                }

                string import = importBuilder.ToString();
                string[] feed_list = import.Split("####################");
                int i = 0;

                foreach (string feed in feed_list)
                {
                    string[] feed_items = feed.Split("||");
                    string url = feed_items[3];

                    Feed tmp = await FeedReader.ReadFeedAsync(url)
                        ?? throw new NullException();
                    Feeds[i.ToString()] = tmp;
                    i++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Imports a selection of <see cref="Feeds"/> to this instance of <see cref="FeedList"/>
        /// </summary>
        /// <param name="path">Complete path to the export, including the name of the file.</param>
        /// <example>ImportList("C:\Users\user\rss_reader\export\export.txt")</example>
        public void ImportList(string path)
        {
            try
            {
                StringBuilder importBuilder = new();
                using (StreamReader F = new(path))
                {
                    // We jump the first line to remove the first separator which separates nothing
                    F.ReadLine();

                    string line;
                    while ((line = F.ReadLine()!) != null) { importBuilder.Append(line); }
                }

                string import = importBuilder.ToString();
                string[] feed_list = import.Split("####################");
                int i = 0;

                foreach (string feed in feed_list)
                {
                    string[] feed_items = feed.Split("||");
                    string url = feed_items[3];

                    Feed tmp = FeedReader.ReadFeed(url)
                        ?? throw new NullException();
                    Feeds[i.ToString()] = tmp;
                    i++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
