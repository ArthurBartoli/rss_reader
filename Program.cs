using Controllers;
using Models;
using rss_reader.models;
using Views;


class Program {
    static void Main(string[] args) {
        var feed_list = new FeedList();
        var view = new ConsoleView();

        feed_list.Feeds.Add(RssReaderController.LoadFeed("https://www.feedforall.com/sample.xml"));
        feed_list.Feeds.Add(RssReaderController.LoadFeed("https://www.feedforall.com/sample-feed.xml"));
        feed_list.Feeds.Add(RssReaderController.LoadFeed("https://www.feedforall.com/blog-feed.xml"));
        feed_list.Feeds.Add(RssReaderController.LoadFeed("http://www.rss-specifications.com/blog-feed.xml"));

        view.ListFeed(feed_list);   
    }
}
