using rss_reader.models;

namespace rss_reader.toolbox
{
    static public class DictTools
    {
        static public string GetKeyFromItem(Dictionary<string, Article> dict, string val)
        {
            foreach (KeyValuePair<string, Article> paire in dict)
            {
                if (paire.Value.Title == val)
                {
                    return paire.Key; 
                }
            }
            return null; 
        }
    }
}
