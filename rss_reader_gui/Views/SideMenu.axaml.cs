using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using rss_reader_gui.ViewModels;
using rss_reader_gui.Models;

namespace rss_reader_gui.Views
{
    public partial class SideMenu : UserControl
    {
        public SideMenu()
        {
            InitializeComponent();
            DataContext = new SideMenuViewModel();
        }

        public void OnExportSelect(object sender, SelectionChangedEventArgs e)
        {
            var selected_export_name = export_selection.SelectedItem as string;
            selected_export.Text = selected_export_name;
            RepositoryCentral.Selected_Export = selected_export_name;
        }
    }
}
