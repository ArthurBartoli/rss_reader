using CommunityToolkit.Mvvm.ComponentModel;
using rss_reader.models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace rss_reader_gui.ViewModels
{
    public partial class SideMenuViewModel : ViewModelBase
    {
        public SideMenuViewModel()
        {
            List<string> FullExportsList = RSSDataManagement.ListExports().Values.Select(_ => _.Item1).ToList();
            ExportItems = new ObservableCollection<string>(FullExportsList);
        }

        public ObservableCollection<string> ExportItems { get; set; }
        [ObservableProperty]
        private string _selectedExport;
    }
}
