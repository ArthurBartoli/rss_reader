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