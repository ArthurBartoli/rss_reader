using Models;
using System.IO;
using System.Text;

namespace rss_reader.models
{
    internal class FeedList
    {
        public List<Feed>? Feeds { get; set; }

        public FeedList()
        {
            Feeds = new List<Feed>();
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
                    foreach (Feed item in Feeds)
                    {
                        F.WriteLine("####################");
                        F.WriteLine(item.Title);
                        F.WriteLine(item.Description);
                        F.WriteLine(item.LastBuildDate);
                        F.WriteLine(item.Link);
                    }
                }
                Console.WriteLine("Hello I was executed :)");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'exportation : " + ex.Message);
            }
        }

        public void ImportList(string path)
        {

        }
    }
}
