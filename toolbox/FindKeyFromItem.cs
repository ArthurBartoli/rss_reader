using rss_reader.models;

namespace rss_reader.toolbox
{
    static public class DictTools
    {
        /// <summary>
        /// Returns the key of a dictionary of Articles corresponding to an article title. 
        /// If multiple keys correspond, then the first encountered will be returned, which is ok
        /// since it would be surprising to have two articles with the same title.
        /// </summary>
        /// <param name="dict">Dict in which to search the key.</param>
        /// <param name="val">Title of the article to search.</param>
        /// <returns>The key as a string.</returns>
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
