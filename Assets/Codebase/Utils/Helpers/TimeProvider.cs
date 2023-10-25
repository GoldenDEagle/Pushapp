using GamePush;
using System;

namespace Assets.Codebase.Utils.Helpers
{
    public static class TimeProvider
    {
        public static DateTime GetServerTime()
        {
            return GP_Server.Time();
        }

        public static DateTime GetLocalTime()
        {
            return DateTime.Now;
        }
    }
}
