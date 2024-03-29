namespace rss_reader.models {
    /// <summary>
    /// Contains all metadata collected from the XML Feed parsing of an article.
    /// This includes title, link, date of publication and description.
    /// The link is meant to be opened in the user's web browser.
    /// </summary>
    public class Article {
        public Article(string title, string link, string pubDate, string description)
        {
            Title = title;
            Link = link;
            PubDate = pubDate;
            Description = description;
        }

        public string Title { get; set; }
        public string Link { get; set; }
        public string PubDate { get; set; }
        public string Description { get; set; }

    }
}
