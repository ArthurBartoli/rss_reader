using Avalonia.Controls;
using Avalonia.Interactivity;
using rss_reader.models;
using rss_reader_gui.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace rss_reader_gui.Views
{
    public partial class SideMenu : UserControl
    {
        public SideMenu()
        {
            InitializeComponent();
            DataContext = new SideMenuViewModel();
            List<string> FullExportsList = RSSDataManagement.ListExports().Values.Select(_ => _.Item1).ToList();
        }
    }
}
