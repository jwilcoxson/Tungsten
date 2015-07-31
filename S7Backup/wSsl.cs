using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tungsten
{
    class sslListing
    {
        wCpu cpu;

        public sslListing (wCpu cpu)
        {
            this.cpu = cpu;
        }

        public void read()
        {
            
        }
    }

    abstract class wSsl
    {
        int id;
        int index;
    }

    class moduleIdentification : wSsl
    {
        public string module;
        public string basicHardware;

        public string version
        {
            get { return V1.ToString() + "." + V2.ToString() + "." + V3.ToString(); }
        }

        private byte V1, V2, V3;

        public string vipaVersion
        {
            get { return vipaV1.ToString() + "." + vipaV2.ToString() + "." + vipaV3.ToString(); }
        }

        private byte vipaV1, vipaV2, vipaV3;


        public moduleIdentification(int recordLength, int numberOfRecords, byte[] data)
        {


            List<byte[]> recordList = new List<byte[]>();

            for (int i = 0; i < numberOfRecords; i++)
            {
                byte[] r = new byte[recordLength];
                Array.Copy(data, i * recordLength, r, 0, recordLength);
                recordList.Add(r);
            }

            module = System.Text.Encoding.ASCII.GetString(recordList[0], 2, 20);
            basicHardware = System.Text.Encoding.ASCII.GetString(recordList[1], 2, 20);
            V1 = recordList[2][25];
            V2 = recordList[2][26];
            V3 = recordList[2][27];

            if (numberOfRecords >= 4)
            {
                vipaV1 = recordList[3][25];
                vipaV2 = recordList[3][26];
                vipaV3 = recordList[3][27];
            }
            

        }

        public void read()
        {

        }
    }
}
