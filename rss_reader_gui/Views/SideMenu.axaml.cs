using Avalonia.Controls;
using rss_reader_gui.ViewModels;
using rss_reader_gui.Models;

namespace rss_reader_gui.Views
{
    /// <summary>
    /// Avalonia view class for handling logic for the side menu.
    /// This menu lists all exports for selection.
    /// </summary>
    public partial class SideMenu : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the View.
        /// </summary>
        public SideMenu()
        {
            InitializeComponent();
            DataContext = new SideMenuViewModel();
        }

        /// <summary>
        /// Handles the SelectionChanged event. Loads the selected export to the <see cref="RepositoryCentral"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A SelectionChangedEventArgs containing the event.</param>
        public void OnExportSelect(object sender, SelectionChangedEventArgs e)
        {
            var selected_export_name = export_selection.SelectedItem as string;
            RepositoryCentral.Selected_Export = selected_export_name;
        }
    }
}
