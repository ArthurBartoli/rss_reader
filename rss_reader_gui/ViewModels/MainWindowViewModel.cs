using rss_reader.models;
using rss_reader_gui.Models;
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
        }
        public FeedList feedList { get; set; }

        public async void LoadFeedList(object sender, SelectedExportChangedEventArgs e)
        {
            feedList = new FeedList();
            string relativePath = @"..\..\..\..\..\rss_reader\export\";
            string absoluteExportDirPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), relativePath));
            string EXPORT_PATH = Path.Combine(absoluteExportDirPath, e.SelectedExport.ToString() + ".txt");
            await feedList.ImportListAsync(EXPORT_PATH);
            List<string> feedNames = new List<string>();
            foreach (string k in feedList.Feeds.Keys)
            {
                feedNames.Add(feedList.Feeds[k].Title);
            }
            Feeds = new ObservableCollection<string>(feedNames);
        }


        private ObservableCollection<string> _feeds;

        public ObservableCollection<string> Feeds
        {
            get { return _feeds; }
            set { SetProperty(ref _feeds, value); }
        }
        /// <summary>
        /// ///////////////
        /// </summary>
        private ObservableCollection<string> _tmp;

        public ObservableCollection<string> tmp
        {
            get { return _tmp; }
            set { SetProperty(ref _tmp, value); }
        }

    }
}
