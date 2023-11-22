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
        foreach (KeyValuePair<string, string> item in RssReaderController.ListExports())
        {
            Console.WriteLine("* " + Path.GetFileNameWithoutExtension(item.Key));
        }
        // GET LIST OF EXPORTS
    }
}
