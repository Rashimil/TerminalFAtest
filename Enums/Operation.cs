using System.ComponentModel;

namespace TerminalFAtest.Enums
{
    // Признак расчета
    public enum OperationEnum : byte
    {
        [Description("Приход")]
        Sell = 1,

        [Description("Возврат прихода")]
        SellRefund = 2,

        [Description("Расход")]
        Buy = 3,

        [Description("Возврат расхода")]
        BuyRefund = 4
    }
}