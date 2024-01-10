using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.Shapes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using rss_reader.models;

namespace rss_reader_gui.Models
{
    public static class RepositoryCentral
    /// <system>
    ///     This is a repo who registers and broadcasts simple information
    ///     for the whole application.
    ///     This is a bad implementation, meant to be replaced with something  
    ///     more sensible in the future.
    /// </system>
    {
        // Events are defined below this class.
        public static event EventHandler<SelectedExportChangedEventArgs> SelectedExportChanged;

        private static string _selectedExport;
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

        private static void OnSelectedExportChanged(string selectedExport)
        {
            SelectedExportChanged?.Invoke(null, new SelectedExportChangedEventArgs(selectedExport));
        }

        private static void SaveSelectedExportToFile(string value)
        {
            // Saves value to common file
            string SELECTED_EXPORT_PATH = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\common\selectedExport.txt");
            using StreamWriter F = new(SELECTED_EXPORT_PATH);
            F.Write(value);
        }

        static RepositoryCentral()
        {
            string SELECTED_EXPORT_PATH = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\common\selectedExport.txt");
            using StreamReader F = new(SELECTED_EXPORT_PATH);
            _selectedExport = F.ReadToEnd();
        }

    }

    public class SelectedExportChangedEventArgs : EventArgs
    {
        public string SelectedExport { get; private set; }

        public SelectedExportChangedEventArgs(string selectedExport)
        {
            SelectedExport = selectedExport;
        }
    }
}
