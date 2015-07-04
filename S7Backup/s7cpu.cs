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

        public void connect(string ipAddress)
        {
            this.connect(ipAddress, 0, 2);
        }

        public void connect(string ipAddress, int rack, int slot)
        {
            MyClient = new S7Client();

            int connectResult = MyClient.ConnectTo(ipAddress, rack, slot);

            if (connectResult == 0)
            {
                Console.WriteLine("Connected to CPU at IP Address " + ipAddress);
                S7Client.S7OrderCode oc = new S7Client.S7OrderCode();
                int orderCodeResult = MyClient.GetOrderCode(ref oc);

                if (orderCodeResult == 0)
                {
                    this.setOrderCode(oc);
                    Console.WriteLine("CPU Order Code:\t" + this.orderCode);
                    S7Client.S7CpuInfo ci = new S7Client.S7CpuInfo();
                    int cpuInfoResult = MyClient.GetCpuInfo(ref ci);

                    if (cpuInfoResult == 0)
                    {

                        this.setCpuInfo(ci);
                        Console.WriteLine("CPU Serial Number:\t" + this.serialNumber);

                        S7Client.S7BlocksList bl = new S7Client.S7BlocksList();
                        int listBlocksResult = MyClient.ListBlocks(ref bl);

                        if (listBlocksResult == 0)
                        {
                            Console.WriteLine("OB Count:\t" + bl.OBCount);
                            Console.WriteLine("FC Count:\t" + bl.FCCount);
                            Console.WriteLine("FB Count:\t" + bl.FBCount);
                            Console.WriteLine("DB Count:\t" + bl.DBCount);
                            Console.WriteLine("SFC Count:\t" + bl.SFCCount);
                            Console.WriteLine("SFB Count:\t" + bl.SFBCount);
                            Console.WriteLine("SDB Count:\t" + bl.SDBCount);

                        }
                        else //Failed to List Blocks
                        {
                            Console.WriteLine("Failed to list blocks. " + listBlocksResult.ToString("X4"));
                        }
                    }
                    else //Failed to get CPU Info
                    {
                        Console.WriteLine("Failed to get CPU info. " + cpuInfoResult.ToString("X4"));
                    }
                }
                else //Failed to get Order Code
                {
                    Console.WriteLine("Failed to get Order Code. " + orderCodeResult.ToString("X4"));
                }
            }
            else //Failed to connect to CPU
            {
                Console.WriteLine("Failed to connect to CPU. " + connectResult.ToString("X4"));
            }
            this.upload();
            MyClient.Disconnect();
        }
        
        public void upload()
        {
            Console.WriteLine("Uploading program blocks... ");
            foreach (s7BlockType blockType in Enum.GetValues(typeof(s7BlockType)))
            {
                ushort[] blockList = new ushort[0x2000];
                int blockCount = blockList.Length;
                MyClient.ListBlocksOfType((int)blockType, blockList, ref blockCount);
                for (int i = 0; i < blockCount; i++)
                {
                    S7Client.S7BlockInfo blockInfo = new S7Client.S7BlockInfo();
                    MyClient.GetAgBlockInfo((int)blockType, blockList[i], ref blockInfo);

                    byte[] buffer = new byte[4096];
                    int bufferSize = buffer.Length;

                    if (blockType != s7BlockType.SFC && blockType != s7BlockType.SFB)
                        MyClient.FullUpload((int)blockType, blockList[i], buffer, ref bufferSize);
                    else
                        bufferSize = 0;

                    byte[] data = new byte[bufferSize];
                    Array.Copy(buffer, data, data.Length);
                    this.addCpuBlock(blockInfo, data);
                }
            }

            Console.WriteLine("Done!");
        }

        public static string cleanString(string s)
        {
            s = System.Text.RegularExpressions.Regex.Replace(s, @"[^\u0020-\u007F]", string.Empty);
            return s;
        }

        public List<s7CpuBlock> blocks;
        private S7Client MyClient;
        
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
            this.orderCode = cleanString(oc.Code);
            this.V1 = oc.V1;
            this.V2 = oc.V2;
            this.V3 = oc.V3;
        }

        public void setCpuInfo(S7Client.S7CpuInfo ci)
        {
            this.moduleTypeName = cleanString(ci.ModuleTypeName);
            this.serialNumber = cleanString(ci.SerialNumber);
            this.ASName = cleanString(ci.ASName);
            this.copyright = cleanString(ci.Copyright);
            this.moduleName = cleanString(ci.ModuleName);
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
            this.name = s7Cpu.cleanString(info.Header);
            this.family = s7Cpu.cleanString(info.Family);
            this.author = s7Cpu.cleanString(info.Author);
            this.codeDate = s7Cpu.cleanString(info.CodeDate);
            this.interfaceDate = s7Cpu.cleanString(info.IntfDate);
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

        public override string ToString()
        {
            return blockType.ToString() + blockNumber.ToString();
        }

        public s7Language language;
        public s7BlockType blockType;
        public string name,
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
            data = new byte[0];

            foreach (s7CpuBlock block in blocks)
            {
                if ((block.data != null) && (block.blockType != s7BlockType.SFC) && (block.blockType != s7BlockType.SFB))
                    data = data.Concat(block.data).ToArray();
            }
        }

         public byte[] data {get; private set;}
    }
}
