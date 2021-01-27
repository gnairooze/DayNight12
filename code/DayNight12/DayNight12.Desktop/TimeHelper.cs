using System;
using System.Collections.Generic;
using System.Text;

namespace DayNight12.Desktop
{
    public class TimeHelper
    {
        public static int CalculateHour(int hour)
        {
            if (hour == 6 || hour == 18)
            {
                return 12;
            }

            return (hour + 6) % 12;
        }

        public static DateTime CalculateDate(DateTime dateTime)
        {
            if (dateTime.Hour < 19)
            {
                return dateTime.Date;
            }
            else
            {
                return dateTime.AddDays(1).Date;
            }
        }

        public static bool IsDay(int hour)
        {
            if(hour >= 7 && hour <= 18)
            {
                return true;
            }

            return false;
        }
    }
}
