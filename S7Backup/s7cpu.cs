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
    public enum s7SubBlockType
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
        public s7Cpu()
        {
            blocks = new List<s7CpuBlock>();
        }
        //TODO: Deconstruct WLD file in to CPU
        public s7Cpu(wldFile w)
        {
            blocks = new List<s7CpuBlock>();
        }
        
        public List<s7CpuBlock> blocks;
        
        public string moduleTypeName,
                      serialNumber,
                      ASName,
                      copyright,
                      moduleName,
                      orderCode;

        public string version
        {
            get { return V1.ToString() + "." + V2.ToString() + "." + V3.ToString(); }
        }

        private byte V1, V2, V3;

        public void addCpuBlock(S7Client.S7BlockInfo block, byte[] data)
        {
            this.blocks.Add(new s7CpuBlock(block, data));
        }

        public void setOrderCode(S7Client.S7OrderCode oc)
        {
            this.orderCode = oc.Code;
            this.V1 = oc.V1;
            this.V2 = oc.V2;
            this.V3 = oc.V3;
        }

        public void setCpuInfo(S7Client.S7CpuInfo ci)
        {
            this.moduleTypeName = ci.ModuleTypeName;
            this.serialNumber = ci.SerialNumber;
            this.ASName = ci.ASName;
            this.copyright = ci.Copyright;
            this.moduleName = ci.ModuleName;
        }
        
    }
    [Serializable]
    public class s7CpuBlock : IComparable<s7CpuBlock>
    {
        public s7CpuBlock() { }
        public s7CpuBlock(S7Client.S7BlockInfo info, byte[] data)
        {
            if (info.BlkType == (int)s7SubBlockType.OB)
                this.blockType = s7BlockType.OB;
            else if (info.BlkType == (int)s7SubBlockType.FC)
                this.blockType = s7BlockType.FC;
            else if (info.BlkType == (int)s7SubBlockType.FB)
                this.blockType = s7BlockType.FB;
            else if (info.BlkType == (int)s7SubBlockType.DB)
                this.blockType = s7BlockType.DB;
            else if (info.BlkType == (int)s7SubBlockType.SFC)
                this.blockType = s7BlockType.SFC;
            else if (info.BlkType == (int)s7SubBlockType.SFB)
                this.blockType = s7BlockType.SFB;
            else if (info.BlkType == (int)s7SubBlockType.SDB)
                this.blockType = s7BlockType.SDB;
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
            this.data = data;
        }

        public int CompareTo(s7CpuBlock b)
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

        public s7Language language;
        public s7BlockType blockType;
        public s7SubBlockType subBlockType;
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
    public class wldFile
    {
        public wldFile(s7Cpu cpu)
        {
            List<s7CpuBlock> blocks = cpu.blocks;

            blocks.Sort();

            foreach (s7CpuBlock block in blocks)
            {
                if ((block.data != null) && (block.blockType != s7BlockType.SFC) && (block.blockType != s7BlockType.SFB))
                    data.Concat(block.data);
            }
        }

        byte[] data;
    }
}
