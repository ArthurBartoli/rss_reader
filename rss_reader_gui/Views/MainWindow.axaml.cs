using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Diagnostics;

namespace rss_reader_gui.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            var isMenuOpen = SideMenu.Width > 0;        // If the menu is opened
            SideMenu.Width = isMenuOpen ? 0 : 200;      // close it, else open it
            Overlay.IsVisible = !isMenuOpen;            // and lift overlay, or display it
        }

        public void Overlay_Click(object sender, PointerPressedEventArgs e)
        {
            SideMenu.Width = 0;          // close the side menu
            Overlay.IsVisible= false;    // lift overlay
        }
    }
}