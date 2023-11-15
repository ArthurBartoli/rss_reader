using Controllers;
using Models;
using rss_reader.models;
using System.Reflection;
using Views;

namespace rss_reader_tests;

public class FeedIO
{
    [Fact]
    public void FeedList_ExportList_ExportIsRightFormat()
    {
        // Arrange
        FeedList feed_list = new FeedList();
        string[] sample_feeds = new string[4] {
            "https://www.feedforall.com/sample.xml",
            "https://www.feedforall.com/sample-feed.xml",
            "https://www.feedforall.com/blog-feed.xml",
            "http://www.rss-specifications.com/blog-feed.xml"
        };

        string solution_dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader_tests");
        string expected_export_path = Path.Combine(solution_dir, "export_test.txt");
        string expected_export;
        using (StreamReader sr = new StreamReader(expected_export_path))
        {
            expected_export = sr.ToString();
        }

        // Act
        foreach (string feed in sample_feeds) { feed_list = RssReaderController.AddFeed(feed_list, feed); }
        string full_path = Path.Combine(solution_dir, "export.txt");
        string actual_export;
        feed_list.ExportList(full_path);
        using (StreamReader sr = new StreamReader(full_path))
        {
            actual_export = sr.ToString();
        }

        // Assess
        Assert.Equal(expected_export, actual_export);
    }
}
