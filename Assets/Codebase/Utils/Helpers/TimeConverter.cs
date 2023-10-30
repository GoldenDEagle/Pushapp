using System;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Codebase.Utils.Helpers
{
    public static class TimeConverter
    {
        // returns time in 00:00
        public static string TimeInMinutes(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time - minutes * 60f);

            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            return formattedTime;
        }

        public static string TimeInHours(double totalSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            return time.ToString("hh':'mm':'ss");
        }

        public static float ParseStringToFloatTime(string formattedTime)
        {
            //string format = "mm:ss";

            if (TimeSpan.TryParse(formattedTime, out TimeSpan ts))
            {
                var doubleTime = ts.TotalSeconds;
                var result = (float) doubleTime;
                return result;
            }
            else
            {
                Debug.Log("Could't parse string to time");
                return 0f;
            }
        }
    }
}
