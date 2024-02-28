using rss_reader.models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace rss_reader_gui.ViewModels
{
    /// <summary>
    /// Avalonia view-model class for handling the logic and data behind the side menu.
    /// This view will display a list of exports handled by the RSSDataManagement object.
    /// </summary>
    public partial class SideMenuViewModel : ViewModelBase
    {
        // TODO: REMOVE THIS
#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "This is the side menu!";
#pragma warning restore CA1822 // Mark members as static

        private ObservableCollection<string> _exports;
        /// <summary>
        /// Gets and sets exports as an <see cref="ObservableCollection{T}">observable collection</see> for listing.
        /// </summary>
        public ObservableCollection<string> Exports
        {
            get { return _exports; }
            set { SetProperty(ref _exports, value); }
        }

        /// <summary>
        /// Initializes a new instance of the SideMenuViewModel class by listing exports and exposing this
        /// list as an <see cref="ObservableCollection{T}">Observable Collection</see>.
        /// </summary>
        public SideMenuViewModel()
        {
            List<string> FullExportsList = RSSDataManagement.ListExports().Values.Select(_ => _.Item1).ToList();
            Exports = new ObservableCollection<string>(FullExportsList);
        }
    }
}
