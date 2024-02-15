using rss_reader.models;
using rss_reader.controllers;

class Program
{
    static void Main()
    {
        FeedList feed_list = new();
        _ = new FeedList();
        string SOLUTION_DIR = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader");
        string EXPORT_DIR = Path.Combine(SOLUTION_DIR, "export\\export.txt");
        string[] sample_feeds = new string[3] {
            "https://www.feedforall.com/sample.xml",
            "https://www.feedforall.com/sample-feed.xml",
            "https://www.feedforall.com/blog-feed.xml"
        };

        foreach (string feed in sample_feeds) { feed_list.AddFeed(feed); }
        feed_list.ExportList(EXPORT_DIR);


        /*        FeedList tmp = new();
                tmp.ImportList(EXPORT_DIR);

                foreach (string key in tmp.Feeds.Keys)
                {
                    Console.WriteLine(key);
                }*/

        RssReaderController.MainMenu();
    }
}
