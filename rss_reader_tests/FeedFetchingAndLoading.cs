using Controllers;
using Models;
using rss_reader.models;
using Views;

namespace rss_reader_tests;

public class FeedFetchingAndLoading
{
    [Fact]
    public void RssReaderController_LoadFeed_FeedCorrectlyParsed()
    {
        // AAA
        // Arrange
        var view = new ConsoleView();
        var feed_list = new FeedList();

        // Act
        feed_list = RssReaderController.AddFeed(feed_list, "https://www.feedforall.com/sample.xml");
        Feed feed_test = feed_list.Feeds.First();

        // Assert
        Assert.Equal("FeedForAll Sample Feed", feed_test.Title);
        Assert.Equal("RSS is a fascinating technology. The uses for RSS are expanding daily. Take a closer look at how various industries are using the benefits of RSS in their businesses.", feed_test.Description);
        Assert.Equal("Computers/Software/Internet/Site Management/Content Management", feed_test.Category);
        Assert.Equal("http://www.feedforall.com/industry-solutions.htm", feed_test.Link);
        Assert.Equal("Tue, 19 Oct 2004 13:39:14 -0400", feed_test.LastBuildDate);
    }

    [Fact]
    public void RssReaderController_LoadFeed_ArticleCorrectlyParsed()
    {
        // AAA
        // Arrange
        var view = new ConsoleView();
        var feed_list = new FeedList();

        // Act
        feed_list = RssReaderController.AddFeed(feed_list, "https://www.feedforall.com/sample.xml");
        Feed feed_test = feed_list.Feeds.First();
        Article article_test = feed_test.Articles[0];

        // Assert
        Assert.Equal("RSS Solutions for Restaurants", article_test.Title);
        Assert.Equal("<b>FeedForAll </b>helps Restaurant's communicate with customers. Let your customers know the latest specials or events.<br>\n<br>\nRSS feed uses include:<br>\n<i><font color=\"#FF0000\">Daily Specials <br>\nEntertainment <br>\nCalendar of Events </i></font>", article_test.Description);
        Assert.Equal("Tue, 19 Oct 2004 11:09:11 -0400", article_test.PubDate);
        Assert.Equal("http://www.feedforall.com/restaurant.htm", article_test.Link);
    }
}