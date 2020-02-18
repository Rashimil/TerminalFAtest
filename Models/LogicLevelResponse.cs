using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Enums;
using TerminalFAtest.Helpers;

namespace TerminalFAtest.Models
{
    // Ответы логического уровня (не всегда необходимы)
    public class LogicLevelResponse
    {
        public LogicLevelResponse()
        {
            this.connectResult = new TerminalConnectResult();
        }
        public void Set(ErrorCodeEnum errorCode)
        {
            this.Code = (byte)errorCode;
            this.Description = EnumHelper.GetTypeDescription(errorCode);
            this.Response = errorCode;
        }
        public byte Code { get; set; } // код 
        public string Description { get; set; } // описание
        public ErrorCodeEnum Response { get; set; } // сама ошибка
        public TerminalConnectResult connectResult; // результат соединения
    }
}
