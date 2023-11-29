namespace Models
{
    class Feed
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string LastBuildDate { get; set; }
        public string Link { get; set; }
        public Dictionary<string, Article> Articles { get; set; }

        // Constructor initiates the list
        public Feed()
        {
            Articles = new Dictionary<string, Article>();
        }

    }
}
