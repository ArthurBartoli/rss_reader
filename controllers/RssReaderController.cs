using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Models;
using rss_reader.models;
using Views;
using Controllers;
using rss_reader.controllers;
using System.IO;

namespace Controllers{
    class RssReaderController
    {
        public static void MainMenu()
        {
            try
            {
                while (true) // Main enclosure
                {
                    ConsoleView.TitleScreen();
                    FeedList tmp = new(); // One FeedList can be loaded per runtime

                    while (true) // Command enclosure
                    {
                        Console.WriteLine("Please enter a command (such as list !)");
                        var input = Console.ReadLine();
                        List<string> command = input != null ? new List<string>(input.Split(" ")) : new List<string> { "main" };

                        Console.WriteLine("-----------------------------------------");
                        switch(command[0])
                        {
                            case "main":
                                Console.Clear();
                                ConsoleView.TitleScreen();
                                break;
                            case "help":
                                Console.WriteLine("Here is a list of available commands");
                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine("* help --> Lists all available commands :)");
                                Console.WriteLine("* list --> Lists all available exports");
                                Console.WriteLine("* exit --> Just exits the program");
                                Console.WriteLine("* main --> Returns to main menu");
                                Console.WriteLine("* load [export_name] --> Loads a feed list into the program");
                                Console.WriteLine("* display (feeds | feed [feed_name] | article [feed_name.article_name])" +
                                    " --> Display feed info or the article of a specific feed");
                                goto case "root pattern";
                            case "root pattern":
                                Console.WriteLine();
                                break;
                            case "exit":
                                return;

                            case "load":
                                Dictionary<string, string> export = RssReaderController.ListExports();
                                tmp.ImportList(export[command[1]]);
                                Console.WriteLine("### Here is the list of feeds in this list :");
                                ConsoleView.ListFeed(tmp);
                                goto case "root pattern";
                                
                            case "list":
                                Dictionary<string, string> exports_list = RssReaderController.ListExports();
                                Console.WriteLine("Here are all available exports :");
                                foreach (string exportsKey in exports_list.Keys)
                                {
                                    Console.WriteLine("* " + exportsKey);
                                }
                                goto case "root pattern";

                            case "display":
                                if (command.Count == 1) 
                                { 
                                    Console.WriteLine("Incorrect command. Type 'help' for syntax.");
                                    goto case "root pattern";
                                }
                                if ((tmp.Feeds != null) && (!tmp.Feeds.Any()))
                                {
                                    Console.WriteLine("No feeds have been loaded yet");
                                }
                                if (command[1] == "feeds") { ConsoleView.ListFeed(tmp); }
                                if (command[1] == "feed")
                                {
                                    string command_end = string.Join(' ', command.Skip(2));
                                    Feed targeted_feed = tmp.Feeds[command_end];
                                    Console.WriteLine("####### " + targeted_feed.Title + " #######");
                                    Console.WriteLine(targeted_feed.Description);
                                    Console.WriteLine(" ---- " + targeted_feed.Link);
                                    Console.WriteLine("* Category : " + targeted_feed.Category);
                                    Console.WriteLine("* Last Build Date : " + targeted_feed.LastBuildDate);
                                    Console.WriteLine("####### ARTICLES");
                                    foreach (string article_title in targeted_feed.Articles.Keys)
                                    {
                                        Console.WriteLine(" * " + article_title);
                                    }
                                }
/*                                if (command[1] == "article")
                                {
                                    String[] target = command[2].Split(".");
                                    Feed feed = tmp.Feeds[target[0]];
                                    Article article = feed.Articles[target[1]];
                                    ConsoleView.DisplayArticle(article);
                                }*/
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
                Console.WriteLine("Error while listing existing exports");
                Console.WriteLine(e.Message);

            }
        }

        public static Dictionary<String, String> ListExports(string exportDirectory = null)
        {
            try
            {
                exportDirectory ??= Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader\export\");
                string[] TXTFiles = Directory.GetFiles(exportDirectory, "*.txt");
                if (TXTFiles.Length == 0)
                {
                    Dictionary<String, String> no_res = new Dictionary<String, String>()
                    {
                        { "No exports found.", "" }
                    };
                    return no_res;
                }

                Dictionary<String, String> res = new Dictionary<String, String>();
                // We create key-value pairs for better storage
                foreach (string TXTFile in TXTFiles)
                {
                    res.Add(Path.GetFileNameWithoutExtension(TXTFile), TXTFile);
                }
                return res;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error while listing existing exports");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static FeedList AddFeed(FeedList feedList, string url)
        {
            Feed newFeed = FeedReader.ReadFeed(url);
            feedList.Feeds[newFeed.Title] = newFeed;
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
                Console.WriteLine("Error while displaying feed");
                Console.WriteLine(e.Message);
            }
        }
    }
}