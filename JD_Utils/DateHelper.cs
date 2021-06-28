using System;
using System.Collections.Generic;
using System.Text;

namespace JD.Utils
{
    public static class DateHelper
    {
        private static long Jan1st1970Ms = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc).Ticks;

        /// <summary>
        /// 返回当前时间的毫秒数, 这个毫秒其实就是自1970年1月1日0时起的毫秒数
        /// </summary>
        public static long currentTimeMillis()
        {
            return (System.DateTime.UtcNow.Ticks - Jan1st1970Ms) / 10000;
        }

        /// <summary>
        /// 从一个代表自1970年1月1日0时起的毫秒数，转换为DateTime (北京时间)
        /// </summary>
        public static System.DateTime getDateTime(long timeMillis)
        {
            return new System.DateTime((long)((timeMillis + 28800000L) * 10000 + Jan1st1970Ms));
        }

        /// <summary>
        /// 从一个代表自1970年1月1日0时起的毫秒数，转换为DateTime (UTC时间)
        /// </summary>
        public static System.DateTime getDateTimeUTC(long timeMillis)
        {
            return new System.DateTime(timeMillis * 10000 + Jan1st1970Ms);
        }
    }
}
