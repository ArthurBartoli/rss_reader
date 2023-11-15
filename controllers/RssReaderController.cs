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

namespace Controllers{
    class RssReaderController
    {

        public static FeedList AddFeed(FeedList feedList, string url) 
        {   
            feedList.Feeds.Add(FeedReader.ReadFeed(url));
            return feedList;
        }

        public static void DisplayFeed(Feed feed)
        {
            ConsoleView viewer = new ConsoleView();    
            try
            {
                foreach (Article item in feed.Articles)
                {
                    viewer.DisplayArticle(item);
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