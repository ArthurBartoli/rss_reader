using Controllers;
using Models;
using Views;

namespace rss_reader_tests;

public class UnitTest1
{
    [Fact]
    public void RssReaderController_LoadFeed_CorrectXML()
    {
        // AAA
        // Arrange
        var view = new ConsoleView();

        // Act
        Feed feed_test = RssReaderController.LoadFeed("https://www.feedforall.com/sample.xml");
        view.DisplayArticle(feed_test.Articles[1]);

        // Assert
    }
}