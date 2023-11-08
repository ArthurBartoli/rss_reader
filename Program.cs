using Controllers;
using Models;
using Views;


class Program {
    static void Main(string[] args) {
        var view = new ConsoleView();
        // var controller = new RssReaderController();
        Feed feed_test = RssReaderController.LoadFeed("https://www.lemonde.fr/rss/une.xml");
        RssReaderController.DisplayFeed(feed_test);
    }
}
