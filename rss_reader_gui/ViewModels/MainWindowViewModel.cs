using Avalonia.Controls;
using rss_reader.models;
using rss_reader_gui.Models;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace rss_reader_gui.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "Welcome to RSS Reader :)!";
#pragma warning restore CA1822 // Mark members as static
        public MainWindowViewModel()
        {
            RepositoryCentral.SelectedExportChanged += LoadFeedList;
            List<string> FullExportsList = new() { "Nothing to say !" };
            Feeds = new ObservableCollection<string>(FullExportsList);
            Articles = new ObservableCollection<string>(FullExportsList);
        }
        public FeedList feedList { get; set; }

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

        public void OnFeedSelect(object sender, SelectionChangedEventArgs e)
        {
            string selection = e.ToString();
            var feedKey = feedList.Feeds.First(kvp => kvp.Value.Equals(selection)).Key;
            Feed selected_feed = feedList.Feeds[feedKey];

            List<string> articleNames = new();
            foreach (Article item in selected_feed.Articles.Values)
            {
                articleNames.Add(item.Title);
            }
            Articles = new ObservableCollection<string>(articleNames);
        }


        private ObservableCollection<string> _article;

        public ObservableCollection<string> Article
        {
            get { return _article; }
            set { SetProperty(ref _article, value); }
        }

        private string _feed;

        public string Feed
        {
            get { return _feed; }
            set { 
                SetProperty(ref _feed, value);
                string selection = Feed;
                var feedKey = feedList.Feeds.First(x => x.Value.Title == selection).Key;
                Feed selected_feed = feedList.Feeds[feedKey];

                List<string> articleNames = new();
                foreach (Article item in selected_feed.Articles.Values)
                {
                    articleNames.Add(item.Title);
                }
                Articles = new ObservableCollection<string>(articleNames);
            }
        }

        private ObservableCollection<string> _articles;

        public ObservableCollection<string> Articles
        {
            get { return _articles; }
            set { SetProperty(ref _articles, value); }
        }

        private ObservableCollection<string> _feeds;

        public ObservableCollection<string> Feeds
        {
            get { return _feeds; }
            set { SetProperty(ref _feeds, value); }
        }

        private ObservableCollection<string> _tmp;

        public ObservableCollection<string> tmp
        {
            get { return _tmp; }
            set { SetProperty(ref _tmp, value); }
        }

    }
}
