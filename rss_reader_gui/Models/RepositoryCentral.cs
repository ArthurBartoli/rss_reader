using System;
using System.IO;

namespace rss_reader_gui.Models
{
    /// <summary>
    /// Central repository for managing and persisting the selected export option throughout the RSS Reader application.
    /// </summary>
    /// <remarks>
    /// This static class acts as a temporary solution for storing application-wide settings, such as the currently selected export option.
    /// It is designed to be replaced with a more robust and scalable solution in the future.
    /// </remarks>
    public static class RepositoryCentral
    {
        /// <summary>
        /// Event triggered when the selected export option changes.
        /// </summary>
        public static event EventHandler<SelectedExportChangedEventArgs> SelectedExportChanged;

        private static string _selectedExport;
        /// <summary>
        /// Gets or sets the currently selected export option.
        /// </summary>
        /// <value>
        /// The name or identifier of the selected export option.
        /// </value>
        /// <remarks>
        /// Setting this property to a new value triggers the <see cref="SelectedExportChanged"/> event and persists the new value to a file.
        /// </remarks>
        public static string Selected_Export
        {
            get => _selectedExport;
            set
            {
                if (_selectedExport != value)
                {
                    _selectedExport = value;
                    OnSelectedExportChanged(_selectedExport);
                    SaveSelectedExportToFile(value);
                }
            }
        }

        /// <summary>
        /// Invokes the <see cref="SelectedExportChanged"/> event
        /// </summary>
        /// <param name="selectedExport">The new selected export option.</param>
        private static void OnSelectedExportChanged(string selectedExport)
        {
            SelectedExportChanged?.Invoke(null, new SelectedExportChangedEventArgs(selectedExport));
        }

        /// <summary>
        /// Persists the selected export option to a file.
        /// </summary>
        /// <param name="value">The selected expot option to save.</param>
        private static void SaveSelectedExportToFile(string value)
        {
            // Saves value to common file
            string SELECTED_EXPORT_PATH = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\common\selectedExport.txt");
            using StreamWriter F = new(SELECTED_EXPORT_PATH);
            F.Write(value);
        }

        /// <summary>
        /// Static constructor to initialize the <see cref="RepositoryCentral"/> class.
        /// </summary>
        /// <remarks>
        /// Reads the previously selected export option from a file and initializes the <see cref="_selectedExport"/> field with it.
        /// </remarks>
        static RepositoryCentral()
        {
            string SELECTED_EXPORT_PATH = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\common\selectedExport.txt");
            using StreamReader F = new(SELECTED_EXPORT_PATH);
            _selectedExport = F.ReadToEnd();
        }

    }

    /// <summary>
    /// Defines data field for the <see cref="RepositoryCentral.SelectedExportChanged"/> event.
    /// </summary>
    public class SelectedExportChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the selected export option.
        /// </summary>
        public string SelectedExport { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedExportChangedEventArgs"/> class.
        /// </summary>
        /// <param name="selectedExport">The new selected export option.</param>
        public SelectedExportChangedEventArgs(string selectedExport)
        {
            SelectedExport = selectedExport;
        }
    }
}
