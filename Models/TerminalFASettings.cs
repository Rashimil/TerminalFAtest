using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalFAtest.Models
{
    // Настройки аппарата
    public class TerminalFASettings
    {
        public TerminalFASettings()
        {
            this.IP = "10.3.72.81";
            this.PORT = 5555;
            this.READ_TIMEOUT = 10;
            this.BUFFER_SIZE = 1;
        }
        public string IP { get; set; } // IP аппарата
        public int PORT { get; set; } // Порт аппарата
        public int READ_TIMEOUT { get; set; } // таймаут чтения с порта (потока), сек
        public int BUFFER_SIZE { get; set; } // размер буфера чтения с порта (потока), байт

    }
}
