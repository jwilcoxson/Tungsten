using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snap7;

namespace S7Backup
{
    class s7Cpu
    {
        public s7OrderCode orderCode;
        public s7CpuInfo cpuInfo;
        public s7CpuBlock[] OB;
        public s7CpuBlock[] FC;
        public s7CpuBlock[] FB;
        public s7CpuBlock[] DB;
        public s7CpuBlock[] SFC;
        public s7CpuBlock[] SFB;
        public s7CpuBlock[] SDB;
        
    }

    class s7CpuBlock
    {
       public  bool exists;
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
