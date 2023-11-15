using Controllers;
using Models;
using rss_reader.models;
using Views;


class Program {
    static void Main(string[] args) {
        var feed_list = new FeedList();
        var view = new ConsoleView();

        RssReaderController.AddFeed(feed_list, "https://www.feedforall.com/sample.xml");
        RssReaderController.AddFeed(feed_list, "https://www.feedforall.com/sample-feed.xml");
        RssReaderController.AddFeed(feed_list, "https://www.feedforall.com/blog-feed.xml");
        RssReaderController.AddFeed(feed_list, "http://www.rss-specifications.com/blog-feed.xml");

        view.ListFeed(feed_list);   
    }
}
