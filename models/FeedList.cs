using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rss_reader.models
{
    internal class FeedList
    {
        public List<Feed>? Feeds { get; set; }

        public FeedList()
        {
            Feeds = new List<Feed>();
        }

        public void ExportList(string path)
        {

        }

        public void ImportList(string path)
        {

        }
    }
}
