using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tungsten
{
    public class wldFile
    {
        public wldFile()
        {

        }
        public wldFile(List<byte> bytes)
        {
            this.data = bytes;
        }

        public wldFile(wCpu cpu)
        {
            List<wCpuBlock> blocks = cpu.blocks;
            blocks.Sort();
            data = new List<byte>();

            foreach (wCpuBlock block in blocks)
            {
                if ((block.data != null) && (block.blockType != wBlockType.SFC) && (block.blockType != wBlockType.SFB))
                    data.AddRange(block.data);
            }
        }

        public List<byte> data { get; private set; }

        public void save()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "WLD files (*.wld)|*.wld|All files (*.*)|*.*";
            dialog.FileName = "S7PROG.wld";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream fs;
                if ((fs = dialog.OpenFile()) != null)
                {
                    try
                    {
                        fs.Write(this.data.ToArray(), 0, this.data.Count);
                    }
                    catch
                    {
                        MessageBox.Show("Error saving file");
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }

        }

        public List<Tuple<wSubBlockType, int, List<byte>>> decode()
        {
            const int BLOCK_TYPE_OFFSET = 5;
            const int BLOCK_NUMBER_OFFSET_HIGH = 6;
            const int BLOCK_NUMBER_OFFSET_LOW = 7;
            const int BLOCK_LENGTH_OFFSET_HIGH = 10;
            const int BLOCK_LENGTH_OFFSET_LOW = 11;
            int currentOffset = 0;
            List<Tuple<wSubBlockType, int, List<byte>>> blockList = new List<Tuple<wSubBlockType, int, List<byte>>>();

            while (currentOffset < this.data.Count)
            {
                wSubBlockType blockType = (wSubBlockType)this.data[currentOffset + BLOCK_TYPE_OFFSET];
                int blockNumber = 256 * this.data[currentOffset + BLOCK_NUMBER_OFFSET_HIGH] +
                                    this.data[currentOffset + BLOCK_NUMBER_OFFSET_LOW];
                int blockLength = 256 * this.data[currentOffset + BLOCK_LENGTH_OFFSET_HIGH] +
                                    this.data[currentOffset + BLOCK_LENGTH_OFFSET_LOW];

                Console.WriteLine("Found " + blockType + blockNumber + " with length " + blockLength);

                blockList.Add(new Tuple<wSubBlockType, int, List<byte>>(blockType, blockNumber, this.data.GetRange(currentOffset, blockLength)));
                currentOffset += blockLength;
            }
            return blockList;
        }

        public bool openFromFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "WLD files (*.WLD)|*.WLD|All files (*.*)|*.*";
            dialog.FileName = "S7PROG.wld";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream fs;
                List<byte> bytes = new List<byte>();
                fs = dialog.OpenFile();
                
                try
                {
                    int b = fs.ReadByte();

                    while (b != -1)
                    {
                        bytes.Add((byte)b);
                        b = fs.ReadByte();
                    }
                }
                catch
                {
                    MessageBox.Show("Error opening file");
                    return false;
                }
                finally
                {
                    fs.Close();
                }

                this.data = bytes;
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
