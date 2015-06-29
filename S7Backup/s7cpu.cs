using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace S7Backup
{
    public enum s7Language
    {
        STL = 0x01,
        FBD = 0x02,
        LAD = 0x03,
        DB = 0x04,
        SCL = 0x05,
        Graph = 0x06,
        SDB = 0x11
    }
    public enum s7BlockType
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
    [Serializable]
    public class s7Cpu
    {
        public s7OrderCode orderCode;
        public s7CpuInfo cpuInfo;
        public List<s7CpuBlock> blocks = new List<s7CpuBlock>();

        public void addCpuBlock(S7Client.S7BlockInfo block, byte[] data)
        {
            if (block.BlkType == (int)s7SubBlockType.OB)
                this.blocks.Add(new s7CpuOB(block, data));
            if (block.BlkType == (int)s7SubBlockType.FC)
                this.blocks.Add(new s7CpuFC(block, data));
            if (block.BlkType == (int)s7SubBlockType.FB)
                this.blocks.Add(new s7CpuFB(block, data));
            if (block.BlkType == (int)s7SubBlockType.DB)
                this.blocks.Add(new s7CpuDB(block, data));
            if (block.BlkType == (int)s7SubBlockType.SFC)
                this.blocks.Add(new s7CpuSFC(block));
            if (block.BlkType == (int)s7SubBlockType.SFB)
                this.blocks.Add(new s7CpuSFB(block));
            if (block.BlkType == (int)s7SubBlockType.SDB)
                this.blocks.Add(new s7CpuSDB(block, data));
        }
        
    }
    [Serializable]
    [XmlInclude(typeof(s7CpuOB)), XmlInclude(typeof(s7CpuFC)), XmlInclude(typeof(s7CpuFB)), XmlInclude(typeof(s7CpuDB)), XmlInclude(typeof(s7CpuSFC)), XmlInclude(typeof(s7CpuSFB)), XmlInclude(typeof(s7CpuSDB))]
    public class s7CpuBlock
    {
        public s7CpuBlock() { }
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
        public string   name,
                        family,
                        author,
                        codeDate,
                        interfaceDate;
        public int  loadSize, 
                    MC7Size,
                    blockNumber,
                    blockFlags,
                    localData,
                    SBBLength,
                    checksum,
                    version;
        public byte[] data;

    }

    public class s7CpuOB : s7CpuBlock
    {
        public s7CpuOB() { }
        public s7CpuOB(S7Client.S7BlockInfo info, byte[] data) : base(info)
        {
            this.blockType = s7BlockType.OB;
            this.data = data;
        }
    }
    public class s7CpuFC : s7CpuBlock
    {
        public s7CpuFC() { }
        public s7CpuFC(S7Client.S7BlockInfo info, byte[] data)
            : base(info)
        {
            this.blockType = s7BlockType.FC;
            this.data = data;
        }
    }
    public class s7CpuFB : s7CpuBlock
    {
        public s7CpuFB() { }
        public s7CpuFB(S7Client.S7BlockInfo info, byte[] data)
            : base(info)
        {
            this.blockType = s7BlockType.FB;
            this.data = data;
        }
    }
    public class s7CpuDB : s7CpuBlock
    {
        public s7CpuDB() { }
        public s7CpuDB(S7Client.S7BlockInfo info, byte[] data)
            : base(info)
        {
            this.blockType = s7BlockType.DB;
            this.data = data;
        }
    }
    public class s7CpuSFC : s7CpuBlock
    {
        public s7CpuSFC() { }
        public s7CpuSFC(S7Client.S7BlockInfo info)
            : base(info)
        {
            this.blockType = s7BlockType.SFC;
        }
    }
    public class s7CpuSFB : s7CpuBlock
    {
        public s7CpuSFB() { }
        public s7CpuSFB(S7Client.S7BlockInfo info)
            : base(info)
        {
            this.blockType = s7BlockType.SFB;
        }
    }
    public class s7CpuSDB : s7CpuBlock
    {
        public s7CpuSDB() { }
        public s7CpuSDB(S7Client.S7BlockInfo info, byte[] data)
            : base(info)
        {
            this.blockType = s7BlockType.SDB;
            this.data = data;
        }
    }
    [Serializable]
    public class s7OrderCode
    {
        public s7OrderCode() { }
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
    [Serializable]
    public class s7CpuInfo
    {
        public s7CpuInfo() { }
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
