using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using rss_reader.models;
using rss_reader_gui.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace rss_reader_gui.Views
{ 
    /// <summary>
    /// Avalonia view class for handling the logic behind the main view
    /// of the application. 
    /// This view will display a list of <see cref="Feed">feeds</see> and a list of <see cref="Article">articles</see>. 
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the View
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            RepositoryCentral.SelectedExportChanged += OnSelectedExportChanged;
        }

        /// <summary>
        /// Handles the SelectedExportChanged event. Loads the selected export option and updates
        /// the feed list accordingly.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A SelectedExportChangedEventArgs containing the event.</param>
        public async void OnSelectedExportChanged(object sender, SelectedExportChangedEventArgs e)
        {
            isLoadedText.Text = e.SelectedExport + " is loaded !";
            FeedList feedList = new();

            string relativePath = @"..\..\..\..\..\rss_reader\export\";
            string absoluteExportDirPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), relativePath));
            string EXPORT_PATH = Path.Combine(absoluteExportDirPath, e.SelectedExport.ToString() + ".txt");
            await feedList.ImportListAsync(EXPORT_PATH);

            List<string> myKeys = [.. feedList.Feeds.Keys];

        }

        /// <summary>
        /// Unsubscribes from the SelectedExportChanged event when the MainWindows is closed,
        /// ensuring proper cleanup.
        /// </summary>
        /// <param name="e">Event data that provides information and event data associated with the Closed event.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            RepositoryCentral.SelectedExportChanged -= OnSelectedExportChanged;
        }

        /// <summary>
        /// Toggles the visibility of the side menu when the LeftCtrl key is pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">KeyEventArgs containing the event data.</param>
        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl) 
            {
                ToggleSideMenu();
            }
        }

        /// <summary>
        /// Toggles the visibility of the side menu when the hamburger button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">RoutedEventArgs containing the event data.</param>
        public void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleSideMenu();
        }

        /// <summary>
        /// Toggles the side menu's visibility, either showing or hiding it based on its current state.
        /// </summary>
        private void ToggleSideMenu()
        {
            var isMenuOpen = SideMenu.Width > 0;        // If the menu is opened
            SideMenu.Width = isMenuOpen ? 0 : 200;      // close it, else open it
            Overlay.IsVisible = !isMenuOpen;            // and lift overlay, or display it
        }

        /// <summary>
        /// Closes the side menu and hides the overlay when the overlay is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">PointerPressedEventArgs contaning the event data.</param>
        public void Overlay_Click(object sender, PointerPressedEventArgs e)
        {
            SideMenu.Width = 0;         // close the side menu
            Overlay.IsVisible = false;  // lift overlay
        }
    }
}