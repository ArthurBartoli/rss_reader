using Controllers;
using Models;
using rss_reader.models;
using Views;


class Program {
    static void Main(string[] args) {
        FeedList feed_list = new();
        FeedList feed_list2 = new();
        string SOLUTION_DIR = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader");
        string EXPORT_DIR = Path.Combine(SOLUTION_DIR, "export\\export.txt");
        /*        FeedList tmp = new();
                tmp.ImportList(EXPORT_DIR);

                foreach (string key in tmp.Feeds.Keys)
                {
                    Console.WriteLine(key);
                }*/

        RssReaderController.MainMenu();
    }
}
