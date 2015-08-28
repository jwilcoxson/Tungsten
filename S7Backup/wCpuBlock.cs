using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;

namespace Tungsten
{
    [Serializable]
    public class wCpuBlock : IComparable<wCpuBlock>
    {
        public wCpuBlock() { }
        public wCpuBlock(S7Client.S7BlockInfo info, byte[] data)
        {
            if (info.BlkType == (int)wSubBlockType.OB)
                this.blockType = wBlockType.OB;
            else if (info.BlkType == (int)wSubBlockType.FC)
                this.blockType = wBlockType.FC;
            else if (info.BlkType == (int)wSubBlockType.FB)
                this.blockType = wBlockType.FB;
            else if (info.BlkType == (int)wSubBlockType.DB)
                this.blockType = wBlockType.DB;
            else if (info.BlkType == (int)wSubBlockType.SFC)
                this.blockType = wBlockType.SFC;
            else if (info.BlkType == (int)wSubBlockType.SFB)
                this.blockType = wBlockType.SFB;
            else if (info.BlkType == (int)wSubBlockType.SDB)
                this.blockType = wBlockType.SDB;
            this.language = (wLanguage)info.BlkLang;
            this.name = wCpu.cleanString(info.Header);
            this.family = wCpu.cleanString(info.Family);
            this.author = wCpu.cleanString(info.Author);
            this.codeDate = wCpu.cleanString(info.CodeDate);
            this.interfaceDate = wCpu.cleanString(info.IntfDate);
            this.loadSize = info.LoadSize;
            this.MC7Size = info.MC7Size;
            this.blockNumber = info.BlkNumber;
            this.blockFlags = info.BlkFlags;
            this.localData = info.LocalData;
            this.SBBLength = info.SBBLength;
            this.checksum = info.CheckSum;
            this.data = data;
        }

        public int CompareTo(wCpuBlock b)
        {
            if (this.blockType == b.blockType)
            {
                return this.blockNumber.CompareTo(b.blockNumber);
            }
            else
            {
                return this.blockType.CompareTo(b.blockType);
            }
        }

        public override string ToString()
        {
            return blockType.ToString() + blockNumber.ToString();
        }

        public wLanguage language;
        public wBlockType blockType;
        public string name,
                       family,
                       author,
                       codeDate,
                       interfaceDate;
        public int loadSize,
                    MC7Size,
                    blockNumber,
                    blockFlags,
                    localData,
                    SBBLength,
                    checksum,
                    version;
        public byte[] data;
    }
}
