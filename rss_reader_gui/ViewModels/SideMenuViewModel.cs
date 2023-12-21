using rss_reader.models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace rss_reader_gui.ViewModels
{
    public partial class SideMenuViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "This is the side menu!";
#pragma warning restore CA1822 // Mark members as static

        private ObservableCollection<string> _exports;

        public ObservableCollection<string> Exports
        {
            get { return _exports; }
            set { SetProperty(ref _exports, value); }
        }

        public SideMenuViewModel()
        {
            List<string> FullExportsList = RSSDataManagement.ListExports().Values.Select(_ => _.Item1).ToList();
            Exports = new ObservableCollection<string>(FullExportsList);
        }
    }
}
