using Controllers;
using Models;
using rss_reader.models;
using Views;


class Program {
    static void Main(string[] args) {
        FeedList feed_list = new FeedList();
        FeedList feed_list2 = new FeedList();
        ConsoleView view = new ConsoleView();

        string[] sample_feeds = new string[4] {
            "https://www.feedforall.com/sample.xml",
            "https://www.feedforall.com/sample-feed.xml",
            "https://www.feedforall.com/blog-feed.xml",
            "http://www.rss-specifications.com/blog-feed.xml"
        };

        foreach (string feed in sample_feeds) { feed_list = RssReaderController.AddFeed(feed_list, feed); }

        string full_path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader\export");
        Console.WriteLine(full_path);
        feed_list.ExportList(full_path);
        feed_list2.ImportList(full_path);

        Console.WriteLine("######");
        view.ListFeed(feed_list2);
    }
}
