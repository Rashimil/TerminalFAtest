using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Enums;

namespace TerminalFAtest.Models.KKTRequest
{
    // Предмет расчета (НЕ ИСПОЛЬЗУЕТСЯ, УСТАРЕЛ)
    public class CheckItemOLD
    {
        public string Name { get; set; } // Наименование предмета расчета
        public decimal Price { get; set; } // Цена за ед. предмета расчета (с учетом скидок и наценок) (тег 1079)
        public ushort Quantity { get; set; } // Количество предмета расчета (временно ushort)
        public VatEnum Vat { get; set; } // Ставка НДС
        public PaymentMethodEnum PaymentMethod { get; set; } // Признак СПОСОБА расчета
        public PaymentObjectEnum PaymentObject { get; set; } /* Признак ПРЕДМЕТА расчета (необязательный параметр) 
        если 15 или 16, то значение тэга 1030 (НАИМЕНОВАНИЕ предмета расчета) должно принимать одно из значений, приведенных в приложении 7 */

        // Далее не совпадает с порядком в доке, но совпадает с успешным примером:
        public string NomenclatureCode { get; set; } /* Код товарной номенклатуры (тег 1162) 
        Массив байт в виде строкового представления, например {0x05, 0xAB, 0x12, 0x08} надо передавать как строку "05AB1208"
        За формирование КТН полностью отвечает ПО верхнего уровня. ККТ в данном случае ретранслирует значения, переданные через команду в тэг 1162 фискального документа */
        public string MeasurementUnit { get; set; } // Единица измерения предмета расчета (тег 1197)
        public decimal Excise { get; set; } // Акциз (тег 1229)
        public string CustomsDeclarationNumber { get; set; } // Номер таможенной декларации (тег 1231) 
        public string CountryCode { get; set; } // Код страны (тег 1230)
        public string CustomReq { get; set; } // Доп. реквизит предмета расчета (тег 1191)

        // Признак агента по предмету расчета
        // Телефон оператора перевода
        // Операция платежного агента
        // Телефон платежного агента
        // Телефон оператора по приему платежей
        // Наименование оператора перевода
        // Адрес оператора перевода
        // ИНН оператора перевода
        // Телефон поставщика
        // Наименование поставщика
        // ИНН поставщика
        
    }
}
