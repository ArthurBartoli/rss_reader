using Controllers;
using Models;
using Views;


class Program {
    static void Main(string[] args) {
        var feed_list = new List<Feed>();
        var view = new ConsoleView();

        feed_list.Add(RssReaderController.LoadFeed("https://www.feedforall.com/sample.xml"));
        feed_list.Add(RssReaderController.LoadFeed("https://www.feedforall.com/sample-feed.xml"));
        feed_list.Add(RssReaderController.LoadFeed("https://www.feedforall.com/blog-feed.xml"));
        feed_list.Add(RssReaderController.LoadFeed("http://www.rss-specifications.com/blog-feed.xml"));

        view.ListFeed(feed_list);   
    }
}
