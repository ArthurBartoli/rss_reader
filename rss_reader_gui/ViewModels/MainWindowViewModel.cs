using System.Collections.Generic;

namespace rss_reader_gui.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "Welcome to RSS Reader :)!";
#pragma warning restore CA1822 // Mark members as static
    }
}
