using ConsoleGUI;
using ConsoleGUI.Controls;
using ConsoleGUI.Space;
using rss_reader.models;

namespace rss_reader.views
{
    /// <summary>
    /// Handles all methods linked to showing something for the user in the console.
    /// Entirely static as it is meant to be a "drawer" class and can be called any time.
    /// </summary>
    static class ConsoleView
    {
        /// <summary>
        /// Draws the title screen "RSS READER" :) 
        ///   ____    ____    ____      ____                       _
        ///  |  _ \  / ___|  / ___|    |  _ \    ___    __ _    __| |
        ///  | |_) | \___ \  \___ \    | |_) |  / _ \  / _` |  / _` |  / _ \ | '__|
        ///  |  _ <   ___) |  ___) |   |  _ <  |  __/ | (_| | | (_| | |  __/ | |
        ///  |_| \_\ |____/  |____/    |_| \_\  \___|  \__,_|  \__,_|  \___| |_|
        /// </summary>
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

        /// <summary>
        /// Displays a quick summary of all important properties about a particular <see cref="Article"/>.
        /// Might be useful for debugging purposes or any CLI development. 
        /// </summary>
        /// <param name="article">Article of interest</param>
        public static void DisplayArticle(Article article)
        {
            Console.WriteLine($"* Titre: {article.Title}");
            Console.WriteLine($"* Content: {article.PubDate}");
            Console.WriteLine($"* Description: {article.Description}");
            Console.WriteLine($"* Link: {article.Link}");
            Console.WriteLine("---------------------\n\n");
        }

        /// <summary>
        /// Displays all <see cref="Feed"/> in a particular <see cref="FeedList"/>.
        /// The typical format is "Title - URL".
        /// Might be useful for debugging purposes or any CLI development. 
        /// </summary>
        /// <param name="feed_list">Feedlist of interest.</param>
        public static void DisplayFeeds(FeedList feed_list)
        {
            Dictionary<string, Feed> feeds = feed_list.Feeds;

            foreach (string key in feeds.Keys)
            {
                Feed item = feeds[key];
                Console.WriteLine($" {key}: " + item.Link);
            }
        }

        /// <summary>
        /// Displays all <see cref="Article"/> of a <see cref="Feed"/> using the <see cref="DisplayArticle(Article)"/> method.
        /// </summary>
        /// <param name="feed">Feed of interest</param>
        public static void DisplayFeed(Feed feed)
        {
            foreach (string key in feed.Articles.Keys)
            {
                ConsoleView.DisplayArticle(feed.Articles[key]);
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
