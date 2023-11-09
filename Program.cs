using Controllers;
using Models;
using Views;


class Program {
    static void Main(string[] args) {
        var view = new ConsoleView();
        // var controller = new RssReaderController();
        Feed feed_test = RssReaderController.LoadFeed("https://www.feedforall.com/sample.xml");
        view.DisplayArticle(feed_test.Articles[1]);
    }
}
