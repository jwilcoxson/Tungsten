using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;

namespace S7Backup
{
    enum s7Language
    {
        STL = 0x01,
        FBD = 0x02,
        LAD = 0x03,
        DB = 0x04,
        SCL = 0x05,
        Graph = 0x06
    }
    enum s7BlockType
    {
        OB = 0x38,
        FC = 0x43,
        FB = 0x45,
        DB = 0x41,
        SFC = 0x44,
        SFB = 0x46,
        SDB = 0x42
    }

    enum s7SubBlockType
    {
        OB = 0x08,
        FC = 0x0C,
        FB = 0x0E,
        DB = 0x0A,
        SFC = 0x0D,
        SFB = 0x0F,
        SDB = 0x0B
    }
    [Serializable()]
    class s7Cpu
    {
        public s7OrderCode orderCode;
        public s7CpuInfo cpuInfo;
        public List<s7CpuOB> OB = new List<s7CpuOB>();
        public List<s7CpuFC> FC = new List<s7CpuFC>();
        public List<s7CpuFB> FB = new List<s7CpuFB>();
        public List<s7CpuDB> DB = new List<s7CpuDB>();
        public List<s7CpuSFC> SFC = new List<s7CpuSFC>();
        public List<s7CpuSFB> SFB = new List<s7CpuSFB>();
        public List<s7CpuSDB> SDB = new List<s7CpuSDB>();

        public void addCpuBlock(S7Client.S7BlockInfo block, byte[] data)
        {
            if (block.BlkType == (int)s7SubBlockType.OB)
                this.OB.Add(new s7CpuOB(block, data));
            if (block.BlkType == (int)s7SubBlockType.FC)
                this.FC.Add(new s7CpuFC(block, data));
            if (block.BlkType == (int)s7SubBlockType.FB)
                this.FB.Add(new s7CpuFB(block, data));
            if (block.BlkType == (int)s7SubBlockType.DB)
                this.DB.Add(new s7CpuDB(block, data));
            if (block.BlkType == (int)s7SubBlockType.SFC)
                this.SFC.Add(new s7CpuSFC(block));
            if (block.BlkType == (int)s7SubBlockType.SFB)
                this.SFB.Add(new s7CpuSFB(block));
            if (block.BlkType == (int)s7SubBlockType.SDB)
                this.SDB.Add(new s7CpuSDB(block));
        }
        
    }

    class s7CpuBlock
    {
        public s7CpuBlock(S7Client.S7BlockInfo info)
        {
            this.language = (s7Language) info.BlkLang;
            this.name = info.Header;
            this.family = info.Family;
            this.author = info.Author;
            this.codeDate = info.CodeDate;
            this.interfaceDate = info.IntfDate;
            this.loadSize = info.LoadSize;
            this.MC7Size = info.MC7Size;
            this.blockNumber = info.BlkNumber;
            this.blockFlags = info.BlkFlags;
            this.localData = info.LocalData;
            this.SBBLength = info.SBBLength;
            this.checksum = info.CheckSum;
        }


        public s7Language language;
        public s7BlockType blockType;
        public string name;
        public string family;
        public string author;
        public string codeDate;
        public string interfaceDate;
        public int loadSize;
        public int MC7Size;
        public int blockNumber;
        public int blockFlags;
        public int localData;
        public int SBBLength;
        public int checksum;
        public int version;
        public byte[] data;

    }

    class s7CpuOB : s7CpuBlock
    {
        public s7CpuOB(S7Client.S7BlockInfo info, byte[] data) : base(info)
        {
            this.blockType = s7BlockType.OB;
            this.data = data;
        }
    }
    class s7CpuFC : s7CpuBlock
    {
        public s7CpuFC(S7Client.S7BlockInfo info, byte[] data)
            : base(info)
        {
            this.blockType = s7BlockType.FC;
            this.data = data;
        }
    }
    class s7CpuFB : s7CpuBlock
    {
        public s7CpuFB(S7Client.S7BlockInfo info, byte[] data)
            : base(info)
        {
            this.blockType = s7BlockType.FB;
            this.data = data;
        }
    }
    class s7CpuDB : s7CpuBlock
    {
        public s7CpuDB(S7Client.S7BlockInfo info, byte[] data)
            : base(info)
        {
            this.blockType = s7BlockType.DB;
            this.data = data;
        }
    }
    class s7CpuSFC : s7CpuBlock
    {
        public s7CpuSFC(S7Client.S7BlockInfo info)
            : base(info)
        {
            this.blockType = s7BlockType.SFC;
        }
    }
    class s7CpuSFB : s7CpuBlock
    {
        public s7CpuSFB(S7Client.S7BlockInfo info)
            : base(info)
        {
            this.blockType = s7BlockType.SFB;
        }
    }
    class s7CpuSDB : s7CpuBlock
    {
        public s7CpuSDB(S7Client.S7BlockInfo info)
            : base(info)
        {
            this.blockType = s7BlockType.SDB;
        }
    }
    class s7OrderCode
    {
        public s7OrderCode(Snap7.S7Client.S7OrderCode oc)
        {
            this.code = oc.Code;
            this.V1 = oc.V1;
            this.V2 = oc.V2;
            this.V3 = oc.V3;
        }

        public string code;
        
        byte V1;
        byte V2;
        byte V3;

        public string version()
        {
            return V1.ToString() + "." + V2.ToString() + "." + V3.ToString();
        }
    }

    class s7CpuInfo
    {
        public s7CpuInfo(Snap7.S7Client.S7CpuInfo info)
        {
            this.moduleTypeName = info.ModuleTypeName;
            this.serialNumber = info.SerialNumber;
            this.ASName = info.ASName;
            this.copyright = info.Copyright;
            this.moduleName = info.ModuleName;
        }

        public string moduleTypeName;
        public string serialNumber;
        public string ASName;
        public string copyright;
        public string moduleName;
    }
}
