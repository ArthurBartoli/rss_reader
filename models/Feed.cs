namespace Models {
    class Feed {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<Article> Articles { get; set; }

        // Constructor initiates the list
        public Feed() {
            Articles = new List<Article>();
        }

    }
}