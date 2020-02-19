using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Helpers;

namespace TerminalFAtest.Units
{
    // Парсер ответов от аппарата НЕ НУЖЕН, ИЗБАВИТЬСЯ ОТ КЛАССА
    public class ResponseParser
    {
        private readonly byte[] START_BYTES = { 0xB6, 0x29 };

        //==============================================================================================================================================

        public bool IsValid(byte[] response)
        {
            if (response.Length < 7) return false;
            if (!(response[0] == START_BYTES[0] && response[1] == START_BYTES[1])) return false;

            int length = response[2];
            length = length >> 8;
            length |= response[3];
            if (response.Length - 6 != length) return false;

            byte[] crc = new CRC16CCITT().ComputeCheckSumBytes(response.Skip(2).Take(response.Length - 4).ToArray());
            if (!(crc[0] == response[response.Length - 2] && crc[1] == response[response.Length - 1])) return false;

            return true;
        }

        //==============================================================================================================================================

        public byte[] GetData(byte[] response) // убрать наверное ибо все ест в классе LogicLevel (вообще от всего класса избавься)
        {
            return response.Skip(4).Take(response.Length - 6).ToArray();
        }

        //==============================================================================================================================================
    }
}
