using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Contest.BL.Extensions
{
    public static class DateTimeExtension
    {
        public static string ParseToDateAndMonth(this DateTime dateTime)
        {
            return dateTime.ToString("M");
        }

        public static string ParseToTimeDifference(this DateTime dateTime)
        {
            var difference = DateTime.UtcNow.Subtract(dateTime);
            if (difference.Days == 0)
            {
                switch (difference.Hours)
                {
                    case 0: return "меньше часа назад";
                    case 1: return "час назад";
                    case 2: return "два часа назад";
                    case 3: return "три часа назад";
                    case 4: return "четыре часа назад";
                    default: return $"сегодня в {dateTime:t}";
                }
            }

            if (difference.Days == 1)
                return $"вчера в {dateTime:t}";

            return $"{dateTime:M} в {dateTime:t}";
        }
    }
}
