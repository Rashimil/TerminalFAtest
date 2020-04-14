using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Extensions;
using TerminalFAtest.Helpers;

namespace TerminalFAtest.Models.KKTResponse
{
    public class GetFnNumberResponse : BaseResponse
    {
        public GetFnNumberResponse(LogicLevel logicLevel) : base(logicLevel)
        {
            var DATA = logicLevel.response.DATA;
            if (DATA != null && DATA.Length == 16)
            {
                this.FN = logicLevel.ConvertFromByteArray.ToString(DATA.XReverse().ToArray()).ToString();
            }
        }

        public string FN { get; set; }
    }
}
