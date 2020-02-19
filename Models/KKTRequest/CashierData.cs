using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalFAtest.Models.KKTRequest
{
    // Данные кассира
    public class CashierData
    {
        public CashierData(string CashierFIO, string CashierINN)
        {
            // Конвертация с удобоваримого формата:
            if (CashierFIO.Length > 64)
                CashierFIO = CashierFIO.Substring(0, 64); // обрезка до 64 символов
            if (CashierINN.Length > 12)
                CashierINN = CashierINN.Substring(0, 12); // обрезка до 12 символов
            if (CashierINN.Length < 12)
                CashierINN = CashierINN.PadLeft(12, ' '); // добивка до 12 символов

            // Заполнение:
            this.CashierFIO = CashierFIO;
            this.CashierINN = CashierINN;
        }
        public string CashierFIO { get; set; } // ФИО Кассира (1021, Не более 64 символов)
        public string CashierINN { get; set; } // ИНН Кассира (1203, Параметр необязательный, если передается, то строго 12 символов)
    }
}
