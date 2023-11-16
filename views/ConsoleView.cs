using Models;

using ConsoleGUI;
using ConsoleGUI.Controls;
using ConsoleGUI.Space;
using System.Xml.Linq;
using rss_reader.models;

namespace Views {
    static class ConsoleView {

        public static void TitleScreen()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("  ____");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("    ____");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("    ____");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("      ____                       _\n");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" |  _ \\");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  / ___|");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  / ___|");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("    |  _ \\    ___    __ _    __| |\n");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" | |_) |");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" \\___ \\");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  \\___ \\");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("    | |_) |  / _ \\  / _` |  / _` |  / _ \\ | '__|\n");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" |  _ <");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   ___) |");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  ___) |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("   |  _ <  |  __/ | (_| | | (_| | |  __/ | |\n");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" |_| \\_\\");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" |____/");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  |____/");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("    |_| \\_\\  \\___|  \\__,_|  \\__,_|  \\___| |_|\n");

            int i = 0;
            while (i < 10)
            {
                Console.WriteLine();
                i++;
            }
        }

        public static void DisplayArticle(Article article) {
            Console.WriteLine($"* Titre: {article.Title}");
            Console.WriteLine($"* Content: {article.PubDate}");
            Console.WriteLine($"* Description: {article.Description}");
            Console.WriteLine($"* Link: {article.Link}");
            Console.WriteLine("---------------------\n\n");
        }

        public static void ListFeed(FeedList feed_list)
        {
            List<Feed> feeds = feed_list.Feeds;

            foreach (Feed item in feeds)
            {
                Console.WriteLine(item.Title + " - " + item.Link);
            }
        }

        public static void InitUI()
        {
            ConsoleManager.Setup();
            ConsoleManager.Resize(new Size(150, 40));
            ConsoleManager.Content = new TextBlock { Text = "Hello world" };
        }
    }
}
