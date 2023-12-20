using rss_reader.controllers;
using System.Text;

namespace rss_reader.models
{
    internal class FeedList
    {
        public Dictionary<string, Feed> Feeds { get; set; }

        public FeedList()
        {
            Feeds = new Dictionary<string, Feed>();
        }

        public void AddFeed(string url)
        {
            Feed newFeed = FeedReader.ReadFeed(url)
                ?? throw new ArgumentNullException("Feed is null, something wrong happened");
            if (Feeds.Count == 0)
            {
                this.Feeds["0"] = newFeed;
                return;
            }

            int maxIndex = this.Feeds.Keys
                .Select(key => int.Parse(key))
                .Max();

            this.Feeds[(maxIndex + 1).ToString()] = newFeed;
        }

        public void ExportList(string path)
        {
            try
            {
                if (Feeds.Count == 0)
                {
                    Console.WriteLine("No feed has been loaded.");
                    return;
                }

                using (StreamWriter F = new StreamWriter(path))
                {
                    foreach (string item in Feeds.Keys)
                    {
                        F.WriteLine("####################");
                        F.WriteLine(Feeds[item].Title + "||");
                        F.WriteLine(Feeds[item].Description + "||");
                        F.WriteLine(Feeds[item].LastBuildDate + "||");
                        F.WriteLine(Feeds[item].Link);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while importing Feed List");
                Console.WriteLine(" !!!!!! " + e.Message);
            }
        }

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
                    while ((line = F.ReadLine()) != null) { importBuilder.Append(line); }
                }

                string import = importBuilder.ToString();
                string[] feed_list = import.Split("####################");
                int i = 0;

                foreach (string feed in feed_list)
                {
                    string[] feed_items = feed.Split("||");
                    string url = feed_items[3];

                    Feed tmp = FeedReader.ReadFeed(url)
                        ?? throw new ArgumentNullException("Feed is null, something wrong happened");
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
