namespace rss_reader.models
{
    static public class RSSDataManagement
    {
        public static Dictionary<String, (string, string)> ListExports(string exportDirectory = "")
        {
            try
            {
                exportDirectory ??= Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\rss_reader\export\");
                string[] TXTFiles = Directory.GetFiles(exportDirectory, "*.txt");
                if (TXTFiles.Length == 0)
                {
                    Dictionary<String, (string, string)> no_res = new()
                    {
                        { "0",  ( "No exports found.", "" ) }
                    };
                    return no_res;
                }

                Dictionary<String, (string, string)> res = new();
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
