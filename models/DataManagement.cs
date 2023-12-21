namespace rss_reader.models
{
    static public class RSSDataManagement
    {
        public static Dictionary<string, (string, string)> ListExports(string exportDirectory = "")
        /// <summary>
        ///     Returns the full list of RSS Feed list exports from a directory of exports, by default the one 
        ///     included in the root.
        /// </summary>
        /// <param name="exportDirectory">Path to the directory, if different from the default one.</param>
        /// <returns>
        ///     A dictionary of <string, (string, string)> which is in the form of <id, (filename, path)>
        /// </returns>
        {
            try
            {
                exportDirectory ??= Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader\export\");
                string[] TXTFiles = Directory.GetFiles(exportDirectory, "*.txt");
                if (TXTFiles.Length == 0)
                {
                    Dictionary<string, (string, string)> no_res = new()
                    {
                        { "0",  ( "No exports found.", "" ) }
                    };
                    return no_res;
                }

                Dictionary<string, (string, string)> res = new();
                int i = 0;
                // We create key-value pairs for better storage
                foreach (string TXTFile in TXTFiles)
                {
                    res.Add(i.ToString(), (Path.GetFileNameWithoutExtension(TXTFile), TXTFile));
                    i++;
                }
                return res;

            }
            catch (Exception e)
            {
                Console.WriteLine(" !!!!!! Error while listing existing exports");
                Console.WriteLine(" !!!!!! " + e.Message);
                Dictionary<String, (string, string)> no_res = new()
                    {
                        { "0",  ( "No exports found.", "" ) }
                    };
                return no_res;
            }
        }
    }
}
