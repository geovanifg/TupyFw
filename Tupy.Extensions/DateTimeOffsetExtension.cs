using System;
using System.Numerics;

namespace Tupy.Extensions
{
    public static class DateTimeOffsetExtension
    {
        public static BigInteger ToBigInteger(this DateTimeOffset self)
        {
            var text = self.ToString("yyyyMMddHHmmssfffzzz");
            text = text.Remove(17, 1);
            text = text.Remove(19, 1);
            BigInteger.TryParse(text, out BigInteger result);
            return result;
        }

        public static string ToStringFull(this DateTimeOffset self)
        {
            var result = self.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
            return result;
        }

        public static string ToStringFull(this DateTimeOffset self, string separator)
        {
            var s = separator ?? "";
            var result = self.ToString($"yyyy{s}MM{s}dd{s}HH{s}mm{s}ss{s}fffzzz");
            return result;
        }
    }
}
