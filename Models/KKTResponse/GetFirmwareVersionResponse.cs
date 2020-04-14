using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Extensions;
using TerminalFAtest.Helpers;

namespace TerminalFAtest.Models.KKTResponse
{
    public class GetFirmwareVersionResponse : BaseResponse
    {
        public GetFirmwareVersionResponse(LogicLevel logicLevel) : base(logicLevel)
        {
            var DATA = logicLevel.response.DATA;
            if (DATA != null)
            {
                this.FirmwareVersion = logicLevel.ConvertFromByteArray.ToString(DATA.XReverse().ToArray()).ToString();
            }

        }
        public string FirmwareVersion { get; set; }
    }
}
