using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Tungsten
{
    public enum wLanguage
    {
        STL = 0x01,
        FBD = 0x02,
        LAD = 0x03,
        DB = 0x04,
        SCL = 0x05,
        Graph = 0x06,
        SDB = 0x11
    }
    public enum wBlockType
    {
        OB = 0x38,
        FC = 0x43,
        FB = 0x45,
        DB = 0x41,
        SFC = 0x44,
        SFB = 0x46,
        SDB = 0x42
    }
    public enum wSubBlockType
    {
        OB = 0x08,
        FC = 0x0C,
        FB = 0x0E,
        DB = 0x0A,
        SFC = 0x0D,
        SFB = 0x0F,
        SDB = 0x0B
    }

    public enum wCpuRunMode {
        Unknown = 0x00,
        Stop = 0x04,
        Run = 0x08
    }

    public class wPlcException : Exception
    {
        public readonly int errorCode;

        public wPlcException()
            : base()
        {
            errorCode = 0x00;
        }

        public wPlcException(string message)
            : base(message)
        {

        }

        public wPlcException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public wPlcException(string message, int errorCode)
            : base(message)
        {
            this.errorCode = errorCode;
        }

        public wPlcException(string message, Exception innerException, int errorCode)
            : base(message, innerException)
        {
            this.errorCode = errorCode;
        }

    }
   
    [Serializable]
    public class wCpu
    {
        public wCpu()
        {
            blocks = new List<wCpuBlock>();
        }
        //TODO: Deconstruct WLD file in to CPU
        public wCpu(wldFile w)
        {
            blocks = new List<wCpuBlock>();
        }

        public void connect(string ipAddress)
        {
            this.connect(ipAddress, 0, 2);
        }

        public void connect(string ipAddress, int rack, int slot)
        {
            MyClient = new S7Client();
            int result = MyClient.ConnectTo(ipAddress, rack, slot);

            if (result == 0)
            {
                Console.WriteLine("Connected to CPU at IP Address " + ipAddress);
                S7Client.S7OrderCode oc = new S7Client.S7OrderCode();
                result = MyClient.GetOrderCode(ref oc);

                if (result == 0)
                {
                    Console.WriteLine("CPU Order Code:\t" + this.orderCode);
                    S7Client.S7BlocksList bl = new S7Client.S7BlocksList();
                    result = MyClient.ListBlocks(ref bl);

                    if (result == 0)
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
                        string error = "Failed to list blocks";
                        throw new wPlcException(error, result);
                    }
                }
                else //Failed to get Order Code
                {
                    string error = "Failed to get Order Code";
                    throw new wPlcException(error, result);
                }
            }
            else //Failed to connect to CPU
            {
                string error = "Failed to connect to CPU";
                throw new wPlcException(error, result);
            }
        }

        public void disconnect()
        {
            int result;
            result = MyClient.Disconnect();

            if (result != 0)
            {
                string error = "Error during PLC disconnect";
                throw new wPlcException(error, result);
            }
        }
        
        public void upload()
        {
            Console.WriteLine("Getting CPU information...");

            S7Client.S7OrderCode oc = new S7Client.S7OrderCode();
            int result = MyClient.GetOrderCode(ref oc);

            if (result == 0)
            {
                this.setOrderCode(oc);
            }
            else
            {
                string error = "Failed to get Order Code";
                throw new wPlcException(error, result);
            }

            S7Client.S7CpuInfo ci = new S7Client.S7CpuInfo();
            result = MyClient.GetCpuInfo(ref ci);

            if (result == 0)
            {
                this.setCpuInfo(ci);
            }
            else
            {
                string error = "Failed to get CPU info";
                throw new wPlcException(error, result);
            }

            Console.WriteLine("Uploading program blocks... ");
            foreach (wBlockType blockType in Enum.GetValues(typeof(wBlockType)))
            {
                ushort[] blockList = new ushort[MAX_BLOCK];
                int blockCount = blockList.Length;
                
                result = MyClient.ListBlocksOfType((int)blockType, blockList, ref blockCount);

                if (result == 0)
                {
                    for (int i = 0; i < blockCount; i++)
                    {
                        S7Client.S7BlockInfo blockInfo = new S7Client.S7BlockInfo();
                        result = MyClient.GetAgBlockInfo((int)blockType, blockList[i], ref blockInfo);
                        
                        if (result == 0)
                        {
                            byte[] buffer = new byte[4096];
                            int bufferSize = buffer.Length;

                            if (blockType != wBlockType.SFC && blockType != wBlockType.SFB)
                            {
                                result = MyClient.FullUpload((int)blockType, blockList[i], buffer, ref bufferSize);

                                if (result != 0)
                                {
                                    string error = "Could not upload " + blockType + blockList[i];
                                    throw new wPlcException(error, result);
                                }
                            }
                            else
                            {
                                bufferSize = 0;
                            }

                            byte[] data = new byte[bufferSize];
                            Array.Copy(buffer, data, data.Length);
                            this.addCpuBlock(blockInfo, data);
                            Console.WriteLine(this.blocks.Last().ToString() + " loaded. Size: " + this.blocks.Last().loadSize + " bytes.");
                        }
                        else
                        {
                            string error = "Could not get Block Info for " + blockType + blockList[i];
                            throw new wPlcException(error, result);
                        }

                    }
                }
                else
                {
                    string error = "Failed to list blocks";
                    throw new wPlcException(error, result);
                }

            }
            Console.WriteLine("Done!");
        }

        public void download(string ipAddress)
        {
            download(ipAddress, 0, 2, true);
        }

        public void download(string ipAddress, int rack, int slot, bool eraseCpu)
        {
            wCpuRunMode rm = wCpuRunMode.Unknown;
            bool skippedSystemData = false;

            if (eraseCpu)
            {
                this.erase();
            }
            
            foreach (wCpuBlock b in this.blocks)
            {
                if (b.blockType == wBlockType.SDB && rm != wCpuRunMode.Stop && !skippedSystemData)
                {
                    rm = this.getCpuRunMode();
                    if (rm != wCpuRunMode.Stop)
                    {
                        string message = "The PLC needs to be stopped to load System Data" + System.Environment.NewLine;
                        message = message + "Would you like to stop the PLC?";
                        DialogResult dr = MessageBox.Show(message, "", MessageBoxButtons.YesNo);
                        
                        if (dr == DialogResult.Yes)
                        {
                            this.setCpuRunMode(wCpuRunMode.Stop);
                        }
                        else
                        {
                            MessageBox.Show("System Data will not be loaded");
                        }
                        
                    }
                }

                if (b.blockType == wBlockType.SDB && skippedSystemData)
                {
                    continue;
                }

                if (b.blockType != wBlockType.SFC && b.blockType != wBlockType.SFB)
                {
                    int result;
                    result = MyClient.Download(b.blockNumber, b.data, b.data.Length);
                    if (result == 0)
                    {
                        Console.WriteLine("Downloaded " + b.ToString());
                    }   
                    else
                    {
                        string error = "Could not download  " + b.ToString();
                        throw new wPlcException(error, result);
                    }
                }
            }

            Console.Write("Download Complete");
        }

        public void erase()
        {
            
            Console.WriteLine("Erasing CPU... ");
            foreach (wBlockType blockType in Enum.GetValues(typeof(wBlockType)))
            {
                ushort[] blockList = new ushort[MAX_BLOCK];
                int blockCount = blockList.Length;
                
                int result;
                result = MyClient.ListBlocksOfType((int)blockType, blockList, ref blockCount);

                if (result == 0)
                {
                    for (int i = 0; i < blockCount; i++)
                    {
                        if (blockType != wBlockType.SFC && blockType != wBlockType.SFB)
                        {
                            result = MyClient.Delete((int)blockType, blockList[i]);

                            if (result == 0)
                            {
                                Console.WriteLine("Deleted " + blockType + blockList[i]);
                            }
                            else
                            {
                                string error = "Could not delete  " + blockType + blockList[i];
                                throw new wPlcException(error, result);
                            }
                        }

                    }
                }
                else
                {
                    string error = "Failed to list blocks";
                    throw new wPlcException(error, result);
                }

            }
            Console.WriteLine("Done!");
        }

        public wCpuRunMode getCpuRunMode()
        {
            int result;
            int rm = (int)wCpuRunMode.Unknown;
            result = MyClient.PlcGetStatus(ref rm);
            wCpuRunMode runMode = (wCpuRunMode)rm;

            if (result != 0)
            {
                string error = "Failed to get Run Mode";
                throw new wPlcException(error, result);
            }
            else
            {
                Console.WriteLine("PLC Run Mode is " + runMode);
            }

            return runMode;

        }

        public void setCpuRunMode(wCpuRunMode runMode)
        {
            int result;

            wCpuRunMode currentRunMode = this.getCpuRunMode();
            
            if (runMode == wCpuRunMode.Run)
            {

                if (currentRunMode == wCpuRunMode.Run)
                {
                    Console.WriteLine("PLC is already in Run");
                }
                else
                {
                    result = MyClient.PlcHotStart();
                    if (result == 0)
                    {
                        Console.WriteLine("PLC has started");
                    }
                    else
                    {
                        string error = "Could not start PLC";
                        throw new wPlcException(error, result);
                    }
                }

            }
            else if (runMode == wCpuRunMode.Stop)
            {

                if (currentRunMode == wCpuRunMode.Stop)
                {
                    Console.WriteLine("PLC is already in Stop");
                }
                else
                {
                    result = MyClient.PlcStop();
                    if (result == 0)
                    {
                        Console.WriteLine("PLC has stopped");
                    }
                    else
                    {
                        string error = "Could not stop PLC";
                        throw new wPlcException(error, result);
                    }
                }
    
            }
            else
            {
                Console.WriteLine("Unknown is not a valid CPU Run Mode");
            }
        }

        public void copyRamToRom()
        {
            int result = MyClient.PlcCopyRamToRom(timeout);
            if (result == 0)
            {
                Console.WriteLine("Copied RAM to ROM");
            }
            else
            {
                string error = "Could not copy RAM to ROM";
                throw new wPlcException(error, result);
            }
        }

        public void compressMemory()
        {
            int result = MyClient.PlcCompress(timeout);
            if (result == 0)
            {
                Console.WriteLine("PLC Memory Compressed");
            }
            else
            {
                string error = "Could not compress PLC memory";
                throw new wPlcException(error, result);
            }
        }

        //TODO figure out all of this SSL stuff
        public void readSslList()
        {
            S7Client.S7SZLList sList = new S7Client.S7SZLList();
            int sCount = MAX_BLOCK;
            int result = MyClient.ReadSZLList(ref sList, ref sCount);
            if (result == 0)
            {
                int recordLength = sList.Header.LENTHDR;
               // List<byte[]> sslList = new List<byte[sList.Header.LENTHDR]>();
                for (int i = 0; i < sList.Header.N_DR; i++)
                {
                    byte [] record = new byte[sList.Header.LENTHDR];
                    
                    System.Array.Copy(sList.Data, i * sList.Header.LENTHDR, record, i * sList.Header.LENTHDR, sList.Header.LENTHDR);
                }

            }
            else
            {
                string error = "Could not read SSL Listing";
                throw new wPlcException(error, result);
            }
        }

        public static string cleanString(string s)
        {
            s = System.Text.RegularExpressions.Regex.Replace(s, @"[^\u0020-\u007F]", string.Empty);
            return s;
        }

        public List<wCpuBlock> blocks;
        private S7Client MyClient;
        
        public string moduleTypeName,
                      serialNumber,
                      ASName,
                      copyright,
                      moduleName,
                      orderCode;
        private const int MAX_BLOCK = 0x2000;
        private const uint timeout = 5000;

        public string version
        {
            get { return V1.ToString() + "." + V2.ToString() + "." + V3.ToString(); }
        }

        private byte V1, V2, V3;

        public void addCpuBlock(S7Client.S7BlockInfo block, byte[] data)
        {
            this.blocks.Add(new wCpuBlock(block, data));
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
            this.language = (wLanguage) info.BlkLang;
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
        public wldFile(wCpu cpu)
        {
            List<wCpuBlock> blocks = cpu.blocks;
            blocks.Sort();
            data = new byte[0];

            foreach (wCpuBlock block in blocks)
            {
                if ((block.data != null) && (block.blockType != wBlockType.SFC) && (block.blockType != wBlockType.SFB))
                    data = data.Concat(block.data).ToArray();
            }
        }

         public byte[] data {get; private set;}
    }
}
