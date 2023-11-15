using Controllers;
using Models;
using rss_reader.models;
using Views;


class Program {
    static void Main(string[] args) {
        var feed_list = new FeedList();
        var view = new ConsoleView();

        string[] sample_feeds = new string[4] { 
            "https://www.feedforall.com/sample.xml", 
            "https://www.feedforall.com/sample-feed.xml", 
            "https://www.feedforall.com/blog-feed.xml",
            "http://www.rss-specifications.com/blog-feed.xml"
        };

        foreach (string feed in sample_feeds) { feed_list = RssReaderController.AddFeed(feed_list, feed); }

        string full_path = Directory.GetCurrentDirectory() + "export.txt";
        feed_list.ExportList(full_path);

        view.ListFeed(feed_list);   
    }
}
