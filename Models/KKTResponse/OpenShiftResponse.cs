using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Helpers;
using TerminalFAtest.Extensions;

namespace TerminalFAtest.Models.KKTResponse
{
    // Ответ на открытие смены
    public sealed class OpenShiftResponse : BaseResponse
    {
        public OpenShiftResponse(LogicLevel logicLevel) : base(logicLevel)
        {
            byte[] DATA = logicLevel.response.DATA;
            if (DATA != null && DATA.Length == 10)
            {             
                var sn = DATA.Take(2).XReverse().ToArray();
                var fd = DATA.Skip(2).Take(4).XReverse().ToArray();
                var fpd = DATA.Skip(6).Take(4).XReverse().ToArray();
                this.ShiftNumber = BitConverter.ToInt16(sn, 0);
                sn.Reverse();
                this.FD = BitConverter.ToUInt32(fd, 0).ToString();
                this.FPD = BitConverter.ToUInt32(fpd, 0).ToString();
            }
        }
        public short ShiftNumber { get; set; } // Номер открытой смены
        public string FD { get; set; } // Номер ФД
        public string FPD { get; set; } // Номер ФПД

        // FN брать из статичных данных
    }
}
