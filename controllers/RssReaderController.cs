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
                    // We ask for user's command
                    while (true) // Command enclosure
                    {
                        Console.WriteLine("Please enter a command (such as exports !)");
                        string command = Console.ReadLine() ?? "main";
                        Console.WriteLine("-----------------------------------------");
                        switch(command)
                        {
                            case "main":
                                Console.Clear();
                                ConsoleView.TitleScreen();
                                break;
                            case "help":
                                Console.WriteLine("Here is a list of available commands");
                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine("* help --> Lists all available commands :)");
                                Console.WriteLine("* exports --> Lists all available exports");
                                Console.WriteLine("* exit --> Just exits the program");
                                Console.WriteLine("* main --> Returns to main menu");
                                Console.WriteLine("Here is a list of available commands");
                                goto default;
                            case "exit":
                                return;
                            case "exports":
                                Dictionary<string, string> exports = RssReaderController.ListExports();
                                Console.WriteLine("Here are all available exports :");
                                foreach (string exportsKey in exports.Keys)
                                {
                                    Console.WriteLine("* " + exportsKey);
                                }
                                goto default;
                            default:
                                Console.WriteLine();
                                break;
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
            feedList.Feeds.Add(FeedReader.ReadFeed(url));
            return feedList;
        }

        public static void DisplayFeed(Feed feed)
        {   
            try
            {
                foreach (Article item in feed.Articles)
                {
                    ConsoleView.DisplayArticle(item);
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