﻿using Avalonia.Controls;
using rss_reader.models;
using rss_reader.toolbox;
using rss_reader_gui.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace rss_reader_gui.ViewModels
{
    /// <summary>
    /// Avalonia view-model class for handling the bridge between logic and modeled data 
    /// behind the main view of the application. 
    /// This view will display a list of <see cref="Feed">feeds</see> and a list of <see cref="Article">articles</see>. 
    /// </summary>
    public partial class MainWindowViewModel : ViewModelBase
    {
        // TODO: REMOVED THIS
#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "Welcome to RSS Reader :)!";
#pragma warning restore CA1822 // Mark members as static

        public Feed _selected_feed;

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class, subscribes to the SelectedExportChanged
        /// event, and initializes the Feeds and Articles collections with default values.
        /// </summary>
        public MainWindowViewModel()
        {
            RepositoryCentral.SelectedExportChanged += LoadFeedList;
            List<string> FullExportsList = new() { "Nothing to say !" };
            Feeds = new ObservableCollection<string>(FullExportsList);
            Articles = new ObservableCollection<string>(FullExportsList);
        }

        /// <summary>
        /// Represents the collection of feeds loaded into the application. 
        /// </summary>
        public FeedList feedList { get; set; }

        /// <summary>
        /// Loads the feed list based on the selected export option and updates the <see cref="Feeds"/> collection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data containing the selected export option.</param>
        public async void LoadFeedList(object sender, SelectedExportChangedEventArgs e)
        {
            // Import all feeds into the feedlist object
            feedList = new FeedList();
            string relativePath = @"..\..\..\..\..\rss_reader\export\";
            string absoluteExportDirPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), relativePath));
            string EXPORT_PATH = Path.Combine(absoluteExportDirPath, e.SelectedExport.ToString() + ".txt");
            await feedList.ImportListAsync(EXPORT_PATH);

            // Displaying the list of available feeds
            List<string> feedNames = new List<string>();
            foreach (string k in feedList.Feeds.Keys)
            {
                feedNames.Add(feedList.Feeds[k].Title);
            }
            Feeds = new ObservableCollection<string>(feedNames);
        }

        /// <summary>
        /// Updates the selected feed and loads articles in response to user selection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data containing the selected feed information.</param>
        public void OnFeedSelect(object sender, SelectionChangedEventArgs e)
        {
            string selection = e.ToString();
            var feedKey = feedList.Feeds.First(kvp => kvp.Value.Equals(selection)).Key;
            _selected_feed = feedList.Feeds[feedKey];

            List<string> articleNames = new();
            foreach (Article item in _selected_feed.Articles.Values)
            {
                articleNames.Add(item.Title);
            }
            Articles = new ObservableCollection<string>(articleNames);
        }


        private string _article;

        /// <summary>
        /// Gets or sets the currently selected article, opening its link in the default browser when set.
        /// </summary>
        public string Article
        {
            get { return _article; }
            set {
                SetProperty(ref _article, value);

                // We get the article from the dict using the Article name 
                string articleKey = DictTools.GetKeyFromItem(_selected_feed.Articles, Article);
                string url = _selected_feed.Articles[articleKey].Link;
                // Then we get the url from this article

                // see https://stackoverflow.com/questions/4580263/how-to-open-in-default-browser-in-c-sharp
                Process.Start("explorer", url);
            }
        }

        /// <summary>
        /// Updates the currently selected feed and its articles based on the provided feed title.
        /// </summary>
        /// <param name="feedTitle">The title of the feed to select.</param>
        private void UpdateSelectedFeedAndArticles(string feedTitle)
        {
            var feedKvp = feedList.Feeds.FirstOrDefault(x => x.Value.Title.Equals(feedTitle));
            if (!feedKvp.Equals(default(KeyValuePair<string, Feed>)))
            {
                _selected_feed = feedKvp.Value;

                var articleNames = _selected_feed.Articles.Values.Select(article => article.Title).ToList();
                Articles = new ObservableCollection<string>(articleNames);
            }
            else
            {
                _selected_feed = null;
                var articleNames = new List<string> { "Something went wrong while selecting the feed." };
                Articles = new ObservableCollection<string>(articleNames);
            }
        }


        private string _feed;
        /// <summary>
        /// Gets or sets the currently selected feed, updating the list of articles when set. 
        /// </summary>
        public string Feed
        {
            get { return _feed; }
            set {
                if (SetProperty(ref _feed, value))
                {
                    UpdateSelectedFeedAndArticles(value);
                }
            }
        }

        private ObservableCollection<string> _articles;
        /// <summary>
        /// Represents the collection of article titles from the currently selected feed.
        /// </summary>
        public ObservableCollection<string> Articles
        {
            get { return _articles; }
            set { SetProperty(ref _articles, value); }
        }

        private ObservableCollection<string> _feeds;
        /// <summary>
        /// Represents the collection of feed titles available in the application.
        /// </summary>
        public ObservableCollection<string> Feeds
        {
            get { return _feeds; }
            set { SetProperty(ref _feeds, value); }
        }

        private ObservableCollection<string> _tmp;
        /// <summary>
        /// A temporary collection for testing or development purposes.
        /// </summary>
        public ObservableCollection<string> tmp
        {
            get { return _tmp; }
            set { SetProperty(ref _tmp, value); }
        }

    }
}
