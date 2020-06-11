﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalFAtest.Helpers
{
    public class DateTimeHelper
    {
        public int timezone_shift = 5; // Сдвиг GMT
        //=======================================================================================================================================
        public int GetTimeZoneShift()
        {
            return timezone_shift;
        }

        //=======================================================================================================================================

        // Конвертация timestamp в unixtime (милисекунды)
        public long ConvertToUnixTimeMilliseconds(DateTime date_time)
        {
            var date = date_time;
            DateTime origin = new DateTime(1970, 1, 1, timezone_shift, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date - origin;
            try
            {
                return Convert.ToInt64(Math.Round(Math.Floor(diff.TotalMilliseconds), 0));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //=======================================================================================================================================

        // // Конвертация timestamp в unixtime (секунды)
        public int ConvertToUnixTimeSeconds(DateTime date_time)
        {
            var date = date_time;
            DateTime origin = new DateTime(1970, 1, 1, timezone_shift, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date - origin;
            try
            {
                return Convert.ToInt32(Math.Round(Math.Floor(diff.TotalSeconds), 0));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //=======================================================================================================================================

        // Конвертация строкового timestamp (01.11.2018 12:18:00) в unixtime
        public int ConvertToUnixTimeSeconds(string date_time)
        {
            try
            {
                var date = DateTime.ParseExact(date_time, "dd.MM.yyyy HH:mm:ss", null); // Случай 09.11.2018 12:18:00
                DateTime origin = new DateTime(1970, 1, 1, timezone_shift, 0, 0, 0);
                TimeSpan diff = date - origin;
                return Convert.ToInt32(Math.Round(Math.Floor(diff.TotalSeconds), 0));
            }
            catch (Exception)
            {
                try
                {
                    var date = DateTime.ParseExact(date_time, "d.MM.yyyy HH:mm:ss", null); // Случай 9.11.2018 12:18:00
                    DateTime origin = new DateTime(1970, 1, 1, timezone_shift, 0, 0, 0);
                    TimeSpan diff = date - origin;
                    return Convert.ToInt32(Math.Round(Math.Floor(diff.TotalSeconds), 0));
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        //=======================================================================================================================================

        // Возвращает текущий день в unixtime
        public long GetCurrentDayToUnixTimeSeconds()
        {
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, timezone_shift, 0, 0, DateTimeKind.Utc);
            DateTime origin = new DateTime(1970, 1, 1, timezone_shift, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date - origin;
            try
            {
                return Convert.ToInt64(Math.Round(Math.Floor(diff.TotalSeconds), 0));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //=======================================================================================================================================

        // Возвращает дату в пригодном для наименования папки формате
        public string DateToFolderName(DateTime dateTime)
        {
            //var date_time = DateTime.UtcNow.AddHours(timezone_shift);
            var date_time = dateTime;
            string Fix(string s)
            {
                if (s.Length == 1) return "0" + s;
                else return s;
            }
            string dd = Fix(date_time.Day.ToString());
            string mm = Fix(date_time.Month.ToString());
            string yyyy = date_time.Year.ToString();
            return dd + "_" + mm + "_" + yyyy;
        }

        //=======================================================================================================================================

        // Конвертация UnixTime в DateTime
        public DateTime UnixtimeToDateTime(int unixtime)
        {
            return new DateTime(1970, 1, 1, timezone_shift, 0, 0, 0).AddSeconds(unixtime);
        }

        //=======================================================================================================================================
    }
}
