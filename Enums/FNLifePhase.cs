using System.ComponentModel;

namespace TerminalFAtest.Enums
{
    // Фазы жизни ФН
    public enum FNLifePhaseEnum : byte
    {
        [Description("Готов к фискализации")]
        ReadyToFiscalisation = 0x01,

        [Description("Фискальный режим")]
        FiscalMode = 0x03,

        [Description("Постфискальный режим, идет передача ФД в ОФД")]
        PostFiscalMode = 0x07,

        [Description("Чтение данных из архива")]
        ReadingArchiveData = 0x0F
    }

}
