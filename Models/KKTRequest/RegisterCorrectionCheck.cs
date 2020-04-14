using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Enums;

namespace TerminalFAtest.Models.KKTRequest
{
    // Данные для регистрации чека коррекции
    public class RegisterCorrectionCheck
    {
        public RegisterCorrectionCheck(OperationEnum operation, decimal TotalSum)
        {
            // Конвертация с удобоваримого формата:
            uint sum = (uint)Math.Truncate(TotalSum * 100); // в копейках
            byte[] sum4 = BitConverter.GetBytes(sum);
            var sum5 = new List<byte>();
            sum5.AddRange(sum4);
            sum5.Add(0x00);

            // Заполнение:
            this.Operation = (byte)operation;
            this.TotalSum = sum5.ToArray();
        }
        public byte Operation { get; set; } // Признак расчета
        public byte[] TotalSum { get; set; } // Итог чека в копейках, упакованный в 5 байт, формат LE
    }
}
