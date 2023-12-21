using Avalonia.Controls;
using rss_reader_gui.ViewModels;

namespace rss_reader_gui.Views
{
    public partial class SideMenu : UserControl
    {
        public SideMenu()
        {
            InitializeComponent();
            DataContext = new SideMenuViewModel();
        }
    }
}
