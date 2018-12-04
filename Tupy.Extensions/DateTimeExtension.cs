using System;

namespace Tupy.Extensions
{
    public static class DateTimeExtension
    {
        public static long ToLong(this DateTime self)
        {
            var text = self.ToString("yyyyMMddHHmmssfff");
            long.TryParse(text, out long result);
            return result;

        }

        public static string ToStringFull(this DateTime self)
        {
            var result = self.ToString("yyyy-MM-ddTHH:mm:ss.fff");
            return result;
        }

        public static string ToStringFull(this DateTime self, string separator)
        {
            var s = separator ?? "";
            var result = self.ToString($"yyyy{s}MM{s}dd{s}HH{s}mm{s}ss{s}fff");
            return result;
        }

        public static string ToStringYYYYMMDD(this DateTime self)
        {
            var result = self.ToString("yyyyMMdd");
            return result;
        }
    }
}
