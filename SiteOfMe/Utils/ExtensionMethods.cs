using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SiteOfMe.Utils
{
    public static class ExtensionMethods
    {
        public static string ToPersianDate(this DateTime date, bool showTime = false)
        {
            var persianCal = new PersianCalendar();
            var retval = string.Empty;

            if (showTime)
            {
                retval += string.Format("({0:D2}:{1:D2}:{2:D2}) ", persianCal.GetHour(date), persianCal.GetMinute(date),
                                        persianCal.GetSecond(date));
            }
            retval += string.Format("{0:D4}/{1:D2}/{2:D2}", persianCal.GetYear(date), persianCal.GetMonth(date),
                                 persianCal.GetDayOfMonth(date));

            return retval;
        }
    }
}