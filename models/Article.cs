namespace rss_reader.models {
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
