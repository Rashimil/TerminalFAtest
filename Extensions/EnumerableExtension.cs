using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalFAtest.Enums;
using TerminalFAtest.Helpers;

namespace TerminalFAtest.Extensions
{
    public static class EnumerableExtension // расширение класса Enumerable
    {
        static bool NeedReverse;
        /// <summary>
        /// Изменяет(или НЕ изменяет) порядок элементов в последовательности, в зависимости от аппаратной платформы
        /// </summary>
        /// <typeparam name="TSource">Тип элементов source.</typeparam>
        /// <param name="source">Последовательность значений, которые следует расставить в обратном порядке</param>
        /// <returns>Последовательность, элементы которой соответствуют элементам входной последовательности, но следуют в противоположном порядке (либо входную последовательность)</returns>
        /// 
        public static IEnumerable<TSource> XReverse<TSource>(this IEnumerable<TSource> source)
        {
            if (ByteOrderHelper.GetSystemByteOrder() == SystemByteOrderEnum.BE)
            {
                return source.Reverse();
            }
            else
            {
                return source;
            }
        }
    }
}

