using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tungsten
{
    [Serializable]
    public class wPlcBookmark
    {
        public wPlcBookmark() { }
        public wPlcBookmark(int index, string bookmarkName, string ipAddress)
        {
            this.index = index;
            this.bookmarkName = bookmarkName;
            this.ipAddress = ipAddress;
        }
        public int index;
        public string bookmarkName;
        public string ipAddress;
    }
}
