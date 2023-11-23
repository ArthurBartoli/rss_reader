using Controllers;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Models;
using rss_reader.models;
using Views;

namespace rss_reader_tests;

public class FeedFetchingAndLoading
{
    [Fact]
    public void RssController_ListExports_NoExportDetected()
    {
        // Arrange
        string solution_dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader_tests");
        string export_dir = Path.Combine(solution_dir, "no_export");

        // Act
        Dictionary<String, String> export_list = RssReaderController.ListExports(export_dir);

        // Assert
        Assert.Equal("", export_list["No exports found."]);
    }
    [Fact]
    public void RssController_ListExports_ListingIsCorrect()
    {
        // Arrange
        string solution_dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader_tests");
        string export_dir= Path.Combine(solution_dir, "export");
        string test_export_path = Path.Combine(export_dir, "export_test.txt");
        string normal_export_path = Path.Combine(export_dir, "export.txt");

        // Act
        Dictionary<String, String> export_list_tmp = RssReaderController.ListExports(export_dir);
        Dictionary<String, String> export_list = export_list_tmp.Keys.OrderBy(k => k).ToDictionary(k => k, k => export_list_tmp[k]); // sorting
        Console.WriteLine(export_list);

        // Assert
        // Asserting that path is good
        Assert.Equal(test_export_path, export_list["export_test"]);
        Assert.Equal(normal_export_path, export_list["export"]);
        // If the keys were wrong, we wouldn't be able to query the path
    }

    [Fact]
    public void FeedList_ImportList_ImportIsCorrect()
    {
        // Arrange
        FeedList feedList = new();
        string solution_dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader_tests");
        string expected_export_path = Path.Combine(solution_dir, "export\\export_test.txt");

        // Act
        feedList.ImportList(expected_export_path);
        Dictionary<string, Feed> feeds = feedList.Feeds;

        // Assert
        // Asserting 1st RSS
        {
            Feed tmp = feeds["FeedForAll Sample Feed"];
            Assert.Equal("FeedForAll Sample Feed", tmp.Title);
            Assert.Equal("RSS is a fascinating technology. The uses for RSS are expanding daily. " +
                "Take a closer look at how various industries are using the benefits of RSS in their businesses.", tmp.Description);
            Assert.Equal("Tue, 19 Oct 2004 13:39:14 -0400", tmp.LastBuildDate);
            Assert.Equal("http://www.feedforall.com/industry-solutions.htm", tmp.Link);
        }

        // Asserting 2nd RSS
        {
            Feed tmp = feeds["Sample Feed - Favorite RSS Related Software & Resources"];
            Assert.Equal("Sample Feed - Favorite RSS Related Software & Resources", tmp.Title);
            Assert.Equal("Take a look at some of FeedForAll's favorite software and resources for learning more about RSS.", tmp.Description);
            Assert.Equal("Mon, 1 Nov 2004 13:17:17 -0500", tmp.LastBuildDate);
            Assert.Equal("http://www.feedforall.com", tmp.Link);
        }

        // Asserting 1st RSS
        {
            Feed tmp = feeds["An RSS Daily News Feed from FeedForAll - RSS Feed Creation."];
            Assert.Equal("An RSS Daily News Feed from FeedForAll - RSS Feed Creation.", tmp.Title);
            Assert.Equal("RSS is a fascinating technology. The uses for RSS are expanding daily. " +
                "Take a closer look at how various industries are using the benefits of RSS in their businesses. " +
                "New information related to RSS feeds and using RSS for marketing is posted on a regular basis.", tmp.Description);
            Assert.Equal("Mon, 15 Mar 2021 08:20:56 -0400", tmp.LastBuildDate);
            Assert.Equal("http://www.feedforall.com/blog.htm", tmp.Link);
        }
    }

    [Fact]
    public void FeedList_ExportList_ExportIsRightFormat()
    {
        // Arrange
        FeedList feed_list = new FeedList();
        string[] sample_feeds = new string[3] {
            "https://www.feedforall.com/sample.xml",
            "https://www.feedforall.com/sample-feed.xml",
            "https://www.feedforall.com/blog-feed.xml"
        };

        string solution_dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader_tests");
        string expected_export_path = Path.Combine(solution_dir, "export\\export_test.txt");
        string expected_export;
        using (StreamReader sr = new(expected_export_path))
        {
            expected_export = sr.ReadToEnd();
        }

        // Act
        foreach (string feed in sample_feeds) { feed_list = RssReaderController.AddFeed(feed_list, feed); }
        string full_path = Path.Combine(solution_dir, "export\\export.txt");
        string actual_export;
        feed_list.ExportList(full_path);
        using (StreamReader sr = new StreamReader(full_path))
        {
            actual_export = sr.ReadToEnd();
        }

        // Assess
        Assert.Equal(expected_export, actual_export);
    }

    [Fact]
    public void RssReaderController_AddFeed_FeedCorrectlyParsed()
    {
        // AAA
        // Arrange
        var feed_list = new FeedList();

        // Act
        feed_list = RssReaderController.AddFeed(feed_list, "https://www.feedforall.com/sample.xml");
        Feed feed_test = feed_list.Feeds["FeedForAll Sample Feed"];

        // Assert
        Assert.Equal("FeedForAll Sample Feed", feed_test.Title);
        Assert.Equal("RSS is a fascinating technology. The uses for RSS are expanding daily. Take a closer look at how various industries are using the benefits of RSS in their businesses.", feed_test.Description);
        Assert.Equal("Computers/Software/Internet/Site Management/Content Management", feed_test.Category);
        Assert.Equal("http://www.feedforall.com/industry-solutions.htm", feed_test.Link);
        Assert.Equal("Tue, 19 Oct 2004 13:39:14 -0400", feed_test.LastBuildDate);
    }

    [Fact]
    public void RssReaderController_AddFeed_ArticleCorrectlyParsed()
    {
        // AAA
        // Arrange
        var feed_list = new FeedList();

        // Act
        feed_list = RssReaderController.AddFeed(feed_list, "https://www.feedforall.com/sample.xml");
        Feed feed_test = feed_list.Feeds["FeedForAll Sample Feed"]; //TODO: les opérateurs qu'a montré Erwan
        Article article_test = feed_test.Articles[0];

        // Assert
        Assert.Equal("RSS Solutions for Restaurants", article_test.Title);
        Assert.Equal("<b>FeedForAll </b>helps Restaurant's communicate with customers. Let your customers know the latest specials or events.<br>\n<br>\nRSS feed uses include:<br>\n<i><font color=\"#FF0000\">Daily Specials <br>\nEntertainment <br>\nCalendar of Events </i></font>", article_test.Description);
        Assert.Equal("Tue, 19 Oct 2004 11:09:11 -0400", article_test.PubDate);
        Assert.Equal("http://www.feedforall.com/restaurant.htm", article_test.Link);
    }
}