using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalFAtest.Helpers
{
    public class FNFlagsHelper // хелпер для расшифровки флагов и предупреждений ФН
    {
        //public Dictionary<int, string> DecodeFNFlags(byte FnFlags)
        public string DecodeFNFlags(byte FnFlags)
        {
            bool[] bits = new bool[8];
            Dictionary<int, string> result = new Dictionary<int, string>();
            string tmp = "";
            BitArray bitArr = new BitArray(BitConverter.GetBytes(FnFlags));
            for (int i = 0; i <= 7; i++)
            {
                bits[i] = ((FnFlags >> i) & 1) != 0;
            }
            List<string> stringlist = new List<string>();
            foreach (var bit in bits)
            {
                if (bit)
                {
                    //tmp += "1";
                    stringlist.Add("1");
                }
                else
                {
                    //tmp += "0";
                    stringlist.Add("0");
                }
            }
            stringlist.Reverse(); // ВРЕМЕННЫЙ РЕВЕРС
            foreach (var item in stringlist)
            {
                if (item == "1")
                {
                    tmp += "1";
                }
                else
                {
                    tmp += "0";
                }
            }
            return tmp;
        }
    }
}
