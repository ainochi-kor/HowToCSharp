using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ex006_struct_IO
{
    class Program
    {
        struct Data
        {
            public int var1;
            public float var2;
        }
        static void Main(string[] args)
        {
            Data[] DataArray = new Data[2];

            DataArray[0].var1 = 7;
            DataArray[0].var2 = 3.14f;

            DataArray[1].var1 = 12;
            DataArray[1].var2 = 0.5f;

            BinaryWriter bw = new BinaryWriter(File.Open("test.txt", FileMode.Create));
            for (int i = 0; i < DataArray.Length; i++)
            {
                bw.Write(DataArray[i].var1);
                bw.Write(DataArray[i].var2);
            }

            bw.Close();

            int var1;
            float var2;

            BinaryReader br = new BinaryReader(File.Open("test.txt", FileMode.Open));

            while(true)
            {
                try
                {
                    var1 = br.ReadInt32();
                    var2 = br.ReadSingle();
                    Console.WriteLine("{0} {1}", var1, var2);
                }
                catch(EndOfStreamException e)
                {
                    br.Close();
                    break;
                }
            }

            
        }
    }
}
