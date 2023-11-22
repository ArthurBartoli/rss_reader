using Controllers;
using Models;
using rss_reader.models;
using Views;


class Program {
    static void Main(string[] args) {
        FeedList feed_list = new FeedList();
        FeedList feed_list2 = new FeedList();
        string SOLUTION_DIR = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader");

        ConsoleView.TitleScreen();
        Console.WriteLine("Select one of the available feed list :");
        foreach (var item in RssReaderController.ListExports())
        {
            Console.WriteLine("* " + Path.GetFileName(item));
        }
        // GET LIST OF EXPORTS
    }
}
