using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Extensions;
using TerminalFAtest.Helpers;

namespace TerminalFAtest.Models.KKTResponse
{
    public class CloseShiftResponse : BaseResponse
    {
        public CloseShiftResponse(LogicLevel logicLevel) : base(logicLevel)
        {
            var DATA = logicLevel.response.DATA;
            if (DATA != null && DATA.Length == 10)
            {
                this.ShiftNumber = BitConverter.ToInt16(DATA.Take(2).ToArray(), 0);
                var fd = DATA.Skip(2).Take(4).XReverse().ToArray();
                var fpd = DATA.Skip(6).Take(4).XReverse().ToArray();
                this.FD = BitConverter.ToUInt32(fd, 0).ToString();
                this.FPD = BitConverter.ToUInt32(fpd, 0).ToString();
            }
        }
        public short ShiftNumber { get; set; } // Номер закрытой смены
        public string FD { get; set; } // Номер ФД
        public string FPD { get; set; } // Номер ФПД

        // FN брать из статичных данных
    }
}
