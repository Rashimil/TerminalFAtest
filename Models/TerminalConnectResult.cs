using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalFAtest.Models
{
    // Ответ на запрос TerminalFA.Initialize()
    public class TerminalConnectResult
    {
        public bool Connected { get; set; }
        public int Code { get; set; } // Код ответа. 0 - ОК, -1 - неуспешно
        public string Description { get; set; } // Описание
    }
}
