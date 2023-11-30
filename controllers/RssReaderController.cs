using Models;
using rss_reader.controllers;
using rss_reader.models;
using System.IO;
using System.Runtime.InteropServices;
using Views;

namespace Controllers
{
    class RssReaderController
    {
        public static void MainMenu()
        {
            try
            {
                while (true) // Main enclosure
                {
                    ConsoleView.TitleScreen();
                    FeedList tmp = new();

                    while (true) // Command enclosure
                    {
                        Console.WriteLine("Please enter a command (such as list !)");
                        var input = Console.ReadLine();
                        List<string> command = input != null ? new List<string>(input.Split(" ")) : new List<string> { "main" };

                        Console.WriteLine("=========================================");
                        switch (command[0])
                        {
                            case "main":
                                Console.Clear();
                                ConsoleView.TitleScreen();
                                break;
                            case "help":
                                RssReaderController.Command_Help(command);
                                goto case "root pattern";
                            case "root pattern":
                                Console.WriteLine();
                                break;
                            case "exit":
                                return;

                            case "load":
                                Dictionary<String, (string, string)> export = RssReaderController.ListExports();
                                tmp.ImportList(export[command[1]].Item2);
                                Console.WriteLine("### Here is the list of feeds in this list :");
                                ConsoleView.ListFeed(tmp);
                                goto case "root pattern";

                            case "list":
                                Dictionary<String, (string, string)> exports_list = RssReaderController.ListExports();
                                Console.WriteLine("Here are all available exports :");
                                foreach (string exportsKey in exports_list.Keys)
                                {
                                    Console.WriteLine($" {exportsKey}: " + exports_list[exportsKey].Item1);
                                }
                                goto case "root pattern";

                            case "display":
                                RssReaderController.Command_Display(command, tmp);
                                goto case "root pattern";

                            default:
                                Console.WriteLine("The command was not understood, please enter another command.");
                                goto case "root pattern";
                        }

                    }
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while listing existing exports");
                Console.WriteLine(" !!!!!! " + e.Message);

            }
        }

        // COMMAND METHODS BELOW
        static void Command_Help(List<string> command)
        {
            List<string> COMMAND_LIST = new()
                {
                    "list", "exit", "main", "load", "display"
                };
            string SOLUTION_DIR = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader");
            string HELP_DIR = Path.Combine(SOLUTION_DIR, "help");

            if (command.Count == 1)
            {
                Console.WriteLine("Here is a list of available commands");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("* help [command]--> Lists all available commands or gives a detailed help");
                Console.WriteLine("* list --> Lists all available exports");
                Console.WriteLine("* exit --> Just exits the program");
                Console.WriteLine("* main --> Returns to main menu");
                Console.WriteLine("* load [export_id] --> Loads a feed list into the program");
                Console.WriteLine("* display (feeds | feed [feed_id] | article [feed_id.article_id])" +
                    " --> Display feed info or the article of a specific feed");
                return;
            }
            if (COMMAND_LIST.Contains(command[1]))
            {
                string path_help_file = Path.Combine(HELP_DIR, command[1]+".txt");
                using (StreamReader F = new(path_help_file))
                {
                    string content = F.ReadToEnd();
                    Console.WriteLine(content);
                }
                return;
            }
            else
            {
                Console.WriteLine("Command was not recognized. Please enter 'help' followed by the command or just 'help'");
                return;
            }
        }

        static void Command_Display(List<string> command, FeedList tmp)
        {
            try
            {
                var possible_commands = new string[3] { "feed", "feeds", "article" };
                if (command.Count == 1 || !possible_commands.Contains(command[1]))
                {
                    Console.WriteLine("You did not specify what you want to display ! Type 'help' for some help on that.");
                    return;
                }
                if ((tmp.Feeds != null) && (!tmp.Feeds.Any()))
                {
                    Console.WriteLine("No feeds have been loaded yet");
                }
                switch (command[1])
                {
                    case "feeds":
                        ConsoleView.ListFeed(tmp);
                        goto default;
                    case "feed":
                        string command_end_feed = string.Join(' ', command.Skip(2));
                        if (command_end_feed == null)
                        {
                            Console.WriteLine("You did not select a feed. Please type 'display feeds' and select a feed to display.");
                            goto default;
                        }
                        Feed targeted_feed = tmp.Feeds[command_end_feed];
                        Console.WriteLine("####### " + targeted_feed.Title + " #######");
                        Console.WriteLine(targeted_feed.Description);
                        Console.WriteLine(" ---- " + targeted_feed.Link);
                        Console.WriteLine("* Category : " + targeted_feed.Category);
                        Console.WriteLine("* Last Build Date : " + targeted_feed.LastBuildDate);
                        Console.WriteLine("####### ARTICLES");
                        foreach (string k in targeted_feed.Articles.Keys)
                        {
                            Console.WriteLine($"  * {k}: " + targeted_feed.Articles[k].Title);
                        }
                        goto default;
                    case "article":
                        string command_end_article = string.Join(' ', command.Skip(2));
                        if (command_end_article == null)
                        {
                            Console.WriteLine("You did not select a feed. Please type 'display feeds' and select a feed to display.");
                            goto default;
                        }
                        String[] target = command_end_article.Split(".");
                        Feed feed = tmp.Feeds[target[0]];
                        Article article = feed.Articles[target[1]];
                        ConsoleView.DisplayArticle(article);
                        goto default;
                    default:
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while displaying RSS feed/article");
                Console.WriteLine(" !!!!!! " + e.Message);
            }
        }

        public static Dictionary<String, (string, string)> ListExports(string exportDirectory = null)
        {
            try
            {
                exportDirectory ??= Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader\export\");
                string[] TXTFiles = Directory.GetFiles(exportDirectory, "*.txt");
                if (TXTFiles.Length == 0)
                {
                    Dictionary<String, (string, string)> no_res = new Dictionary<String, (string, string)>()
                    {
                        { "0",  ( "No exports found.", "" ) }
                    };
                    return no_res;
                }

                Dictionary<String, (string, string)> res = new Dictionary<String, (string, string)>();
                int i = 0;
                // We create key-value pairs for better storage
                foreach (string TXTFile in TXTFiles)
                {
                    res.Add(i.ToString(), ( Path.GetFileNameWithoutExtension(TXTFile), TXTFile ));
                    i++;
                }
                return res;

            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while listing existing exports");
                Console.WriteLine(" !!!!!! " + e.Message);
                return null;
            }
        }

        public static FeedList AddFeed(FeedList feedList, string url)
        {
            Feed newFeed = FeedReader.ReadFeed(url);
            if (feedList.Feeds.Count() == 0)
            {
                feedList.Feeds["0"] = newFeed;
                return feedList;
            }

            int maxIndex = feedList.Feeds.Keys
                .Select(key => int.Parse(key))
                .Max();

            feedList.Feeds[(maxIndex + 1).ToString()] = newFeed;

            return feedList;
        }

        public static void DisplayFeed(Feed feed)
        {
            try
            {
                foreach (string key in feed.Articles.Keys)
                {
                    ConsoleView.DisplayArticle(feed.Articles[key]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while displaying feed");
                Console.WriteLine(" !!!!!! " + e.Message);
            }
        }
    }
}