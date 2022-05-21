using System;

namespace HT.Web.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDeTime(this DateTime dt)
        {
            var utc = dt.ToUniversalTime();
            var timeInfo = TimeZoneInfo.Local;
            
            return TimeZoneInfo.ConvertTimeFromUtc(utc, timeInfo);
        }
    }
}