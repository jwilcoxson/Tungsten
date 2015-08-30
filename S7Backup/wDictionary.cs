using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tungsten
{
    class wDictionary
    {
        /*
         * Error Codes Dictionaries
         */

        public static readonly Dictionary<int, string> isoTcpErrorCodes = new Dictionary<int, string>
        {
            {0x01, "ISO Connection Error"},
            {0x02, "ISO Disconnection Error"},
            {0x03, "Malformed PDU supplied"},
            {0x04, "Bad data size supplied to Send/Recieve function"},
            {0x05, "Null pointer supplied"},
            {0x06, "Short packet recieved"},
            {0x07, "Too many packets without EoT flag"},
            {0x08, "The sum of fragmented data exceeds the maximum packet size"},
            {0x09, "A send error occured"},
            {0x0A, "A recieve error occured"},
            {0x0B, "Invalid TSAP parameters supplied"},
            {0x0C, "Reserved"},
            {0x0D, "Reserved"},
            {0x0E, "Reserved"},
            {0x0F, "Reserved"}
        };

        public static readonly Dictionary<int, string> clientErrorCodes = new Dictionary<int, string>
        {
            {0x01, "Error during PDU negogiation"},
            {0x02, "Invalid parameter supplied to the current function"},
            {0x03, "A job is pending, there is an asynchronous function in progress"},
            {0x04, "More than 20 items were passed to a multi read/write function"},
            {0x05, "Invalid word length parameter supplied to the current function"},
            {0x06, "Partial data was written, the target area is smaller than the data size supplied"},
            {0x07, "A multi read/write functions has a data size that exceeds the PDU size"},
            {0x08, "Invalid answer from the PLC"},
            {0x09, "An out of range address was specified"},
            {0x0A, "Invalid transport size parameter supplied to the current function"},
            {0x0B, "Invalid data size parameter supplied to the current fucntion"},
            {0x0C, "Item requested was not found in the PLC"},
            {0x0D, "Invalid value supplied to the current function"},
            {0x0E, "PLC cannot be started"},
            {0x0F, "PLC is already in Run"},
            {0x10, "PLC cannot be stopped"},
            {0x11, "Cannot copy RAM to ROM. The PLC is running or doesn't support this function"},
            {0x12, "Cannot compress memory. The PLC is running or doesn't support this function"},
            {0x13, "PLC is already in Stop"},
            {0x14, "Function not available"},
            {0x15, "Block upload sequence failed"},
            {0x16, "Invalid data size recieved from the PLC"},
            {0x17, "Invalid block type suppplied to the current function"},
            {0x18, "Invalid block supplied to the current function"},
            {0x19, "Invalid block size supplied to the current function"},
            {0x1A, "Block download sequence failed"},
            {0x1B, "Block Insert command refused"},
            {0x1C, "Block Delete command refused"},
            {0x1D, "This operation is password protected"},
            {0x1E, "Invalid password supplied"},
            {0x1F, "There is no password to set or clear"},
            {0x20, "Job timeout"},
            {0x21, "Partial data was read, the souce areas is greater than the data size supplied"},
            {0x22, "The buffer supplied is too small"},
            {0x23, "Function refused by PLC"},
            {0x24, "Invalid parameter value supplied to Get/Set parameter"},
            {0x25, "Cannot perform. The client is destroying"},
            {0x26, "Cannot change parameter while connected"}
        };

        public static readonly Dictionary<wAddressLengthType, int> addressLengths = new Dictionary<wAddressLengthType, int>
        {
            {wAddressLengthType.Byte, 1},
            {wAddressLengthType.Word, 2},
            {wAddressLengthType.Double, 4}
        };
    }
}
