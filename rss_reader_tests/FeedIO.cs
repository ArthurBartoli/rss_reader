using Controllers;
using Models;
using rss_reader.models;
using System.Reflection;
using System.Runtime.InteropServices;
using Views;

namespace rss_reader_tests;

public class FeedIO
{
    [Fact]
    public void FeedList_ExportList_ExportIsRightFormat()
    {
        // Arrange
        Console.WriteLine("We start testing IO");
        FeedList feed_list = new FeedList();
        Console.WriteLine(1);
        string[] sample_feeds = new string[4] {
            "https://www.feedforall.com/sample.xml",
            "https://www.feedforall.com/sample-feed.xml",
            "https://www.feedforall.com/blog-feed.xml",
            "http://www.rss-specifications.com/blog-feed.xml"
        };

        Console.WriteLine(2);
        string solution_dir = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader_tests");
        string expected_export_path = Path.Combine(solution_dir, "export_test.txt");
        string expected_export;
        using (StreamReader sr = new StreamReader(expected_export_path))
        {
            expected_export = sr.ToString();
        }

        Console.WriteLine(3);
        // Act
        foreach (string feed in sample_feeds) { feed_list = RssReaderController.AddFeed(feed_list, feed); }
        Console.WriteLine();
        string full_path = Path.Combine(solution_dir, "export.txt");
        string actual_export;
        Console.WriteLine(4);
        feed_list.ExportList(full_path);
        Console.WriteLine(5);
        using (StreamReader sr = new StreamReader(full_path))
        {
            actual_export = sr.ToString();
        }
        Console.WriteLine(6);

        // Assess
        Assert.Equal(expected_export, actual_export);
    }
}
