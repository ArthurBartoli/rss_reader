using Models;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;

namespace rss_reader.models
{
    internal class FeedList
    {
        public Dictionary<string, Feed>? Feeds { get; set; }

        public FeedList() 
        { 
            Feeds = new Dictionary<string, Feed>();
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
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'exportation : " + ex.Message);
            }
        }

        public void ImportList(string path)
        {
            try
            {
                StringBuilder importBuilder = new StringBuilder();
                using (StreamReader F = new StreamReader(path))
                {
                    // We jump the first line to remove the first separator which separates nothing
                    F.ReadLine(); 

                    string line;
                    while ((line = F.ReadLine()) != null)
                    {
                        importBuilder.Append(line);
                    }
                }

                string import = importBuilder.ToString();
                string[] feed_list = import.Split("####################");
                foreach (string feed in feed_list)
                {
                    Feed tmp = new();
                    string[] feed_items = feed.Split("||");
                    
                    tmp.Title = feed_items[0];
                    tmp.Description = feed_items[1];
                    tmp.LastBuildDate = feed_items[2];
                    tmp.Link = feed_items[3];

                    Feeds[tmp.Title] = tmp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
