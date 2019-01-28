using System;

namespace eBayPulse.Tools
{
    public static class ExtencionMethods
    {
        public static DateTime ConvertFromUnixTimestamp(this long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}



