using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Tungsten
{
    public partial class Monitor : Form
    {
        wCpu myCpu;
        public Monitor(wCpu myCpu)
        {
            InitializeComponent();
            this.myCpu = myCpu;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (wDataGridViewMonitorRow r in this.dataGridView1.Rows)
            {
                if (r.Cells["address"].Value != null && r.addressIsValid)
                {
                    byte[] result;
                    switch (r.address.addressType)
                    {
                        case wAddressType.I:
                        default:
                            result = myCpu.readI(r.address.address, wDictionary.addressLengths[r.address.addressLengthType]);
                            break;
                        case wAddressType.M:
                            result = myCpu.readM(r.address.address, wDictionary.addressLengths[r.address.addressLengthType]);
                            break;
                        case wAddressType.Q:
                            result = myCpu.readQ(r.address.address, wDictionary.addressLengths[r.address.addressLengthType]);
                            break;
                    }
                    setValueCell(r, result);
                }
                else
                {
                    r.Cells["value"].Value = "";
                }
            }
        }

        private void setValueCell(DataGridViewRow r, byte[] data)
        {
            List<byte> a = new List<byte>();

            foreach (byte b in data)
            {
                a.Add(b);
            }

            if (r.Cells["dataType"].FormattedValue.ToString() == "Boolean")
            {
                string s = "";
                foreach (byte b in a)
                {
                    s += Convert.ToString(b, 2).PadLeft(8, '0');
                }
                r.Cells["value"].Value = s;
            }
            else if (r.Cells["dataType"].FormattedValue.ToString() == "Integer")
            {
                while (a.Count < 2)
                {
                    a.Insert(0, 0);
                }
                r.Cells["value"].Value = BitConverter.ToUInt16(a.ToArray(), 0);
            }
            else if (r.Cells["dataType"].FormattedValue.ToString() == "Signed")
            {
                while (a.Count < 2)
                {
                    a.Insert(0, 0);
                }
                r.Cells["value"].Value = BitConverter.ToInt16(a.ToArray(), 0);
            }
            else if (r.Cells["dataType"].FormattedValue.ToString() == "Hex")
            {
                string s = "";
                foreach (byte b in a)
                {
                    s += Convert.ToString(b, 16).PadLeft(2, '0');
                }
                r.Cells["value"].Value = "0x" + s.ToUpper();
            }
            else if (r.Cells["dataType"].FormattedValue.ToString() == "Float")
            {
                while (a.Count < 4)
                {
                    a.Insert(0, 0);
                }
                if (BitConverter.IsLittleEndian)
                {
                    a.Reverse();
                }
                r.Cells["value"].Value = BitConverter.ToSingle(a.ToArray(), 0);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "address")
            {
                DataGridViewCell addressCell = dataGridView1.Rows[e.RowIndex].Cells["address"];
                wDataGridViewMonitorRow row = (wDataGridViewMonitorRow)addressCell.OwningRow;
                
                if (addressCell.Value != null)
                {
                    String addressValue = addressCell.Value.ToString();
                    
                    Match m = Regex.Match(addressValue, @"\b");
                    if (m.Success)
                    {
                        addressValue = addressValue.ToString().ToUpper();
                    }

                    m = Regex.Match(addressValue, @"^[I|Q|M]");
                    if (m.Success)
                    {
                        wAddressType addressType;
                        switch (m.Value)
                        {
                            case "I":
                            default:
                                addressType = wAddressType.I;
                                break;
                            case "Q":
                                addressType = wAddressType.Q;
                                break;
                            case "M":
                                addressType = wAddressType.M;
                                break;
                        }

                        string addressTypeValue = addressValue.Substring(1);
                        m = Regex.Match(addressTypeValue, @"^[B|W|D]");
                        if (m.Success)
                        {
                            wAddressLengthType addressLengthType;
                            switch (m.Value)
                            {
                                case "B":
                                default:
                                    addressLengthType = wAddressLengthType.Byte;
                                    row.setByteOptions();
                                    break;
                                case "W":
                                    addressLengthType = wAddressLengthType.Word;
                                    break;
                                case "D":
                                    addressLengthType = wAddressLengthType.Double;
                                    break;
                            }

                            string addressOffset = addressTypeValue.Substring(1);
                            m = Regex.Match(addressOffset, @"^[0-9]+$");
                            if (m.Success)
                            {
                                int address = Int16.Parse(addressOffset);
                                wPlcAddress a = new wPlcAddress(addressType, addressLengthType, address);
                                row.address = a;
                                row.addressIsValid = true;
                                addressCell.Style.BackColor = Color.White;
                            }
                            else
                            {
                                row.addressIsValid = false;
                                addressCell.Style.BackColor = Color.IndianRed;
                            }
                        }
                        else
                        {
                            row.addressIsValid = false;
                            addressCell.Style.BackColor = Color.IndianRed;
                        }
 
                    }
                    else
                    {
                        row.addressIsValid = false;
                        addressCell.Style.BackColor = Color.IndianRed;
                    }

                    addressCell.Value = addressValue;
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "dataType")
            {

            }
        }

        
    }

    class wPlcAddress
    {
        public readonly wAddressType addressType;
        public readonly wAddressLengthType addressLengthType;
        public readonly int address;
        public readonly int bit;

        public wPlcAddress(wAddressType addressType, wAddressLengthType addressLengthType, int address)
        {
            this.addressType = addressType;
            this.addressLengthType = addressLengthType;
            this.address = address;
        }

        public wPlcAddress(wAddressType addressType, int address, int bit)
        {
            this.addressType = addressType;
            this.address = address;
            this.bit = bit;
        }
    }

    class wDataGridViewMonitorRow : DataGridViewRow
    {
        public wPlcAddress address;
        public bool addressIsValid = false;
        public override object Clone()
        {
            var clonedRow = base.Clone() as wDataGridViewMonitorRow;
            return clonedRow;
        }
        public void setByteOptions()
        {
            DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)this.Cells["dataType"];
            cell.Items.Clear();
            cell.Items.AddRange("Boolean", "Integer", "Signed", "Hex");
        }

        public void setWordOptions()
        {
            DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)this.Cells["dataType"];
            cell.Items.Clear();
            cell.Items.AddRange("Boolean", "Integer", "Signed", "Hex");
        }

        public void setDoubleOptions()
        {
            DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)this.Cells["dataType"];
            cell.Items.Clear();
            cell.Items.AddRange("Boolean", "Integer", "Signed", "Hex", "Float");
        }
    }

    public class wDataGridMonitorView : DataGridView
    {
        public wDataGridMonitorView()
        {
            RowTemplate = new wDataGridViewMonitorRow();
        }
    }

    enum wAddressType
    {
        I, Q, M
    }

    enum wAddressLengthType
    {
        Bit, Byte, Word, Double
    }
}
