using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalFAtest.Extensions
{
    public static class DateTimeExtension // расширение класса DateTime
    {
        //==============================================================================================================================================
        public static string XToString(this DateTime source) // возвращает дату и время как строку в корректном виде на deb системах (в виде dd.mm.yyyy hh:mm:ss)
        {
            string Fix(string s)
            {
                if (s.Length == 1) return "0" + s;
                else return s;
            }
            string day = Fix(source.Day.ToString());
            string mounth = Fix(source.Month.ToString());
            string year = source.Year.ToString();
            string hh = Fix(source.Hour.ToString());
            string mm = Fix(source.Year.ToString());
            string ss = Fix(source.Year.ToString());

            return day + "." + mounth + "." + year + " " + hh + ":" + mm + ":" + ss;
        }

        //==============================================================================================================================================
        public static long TimestampToUnixtime(this string timestamp) // недоделано
        {
            DateTime dateTime = DateTime.ParseExact(timestamp, "ddMMyyyy HHmmss", null);
            return 1;
        }

        //==============================================================================================================================================
    }
}
