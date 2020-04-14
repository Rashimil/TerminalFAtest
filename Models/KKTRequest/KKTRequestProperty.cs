using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalFAtest.Models.KKTRequest
{
    // Элемент запроса в ККТ (только для элементов TLV)
    public class KKTRequestProperty<T>
    {
        public int TAG { get; set; }
        public T USER_VALUE { get; set; }
    }
}
