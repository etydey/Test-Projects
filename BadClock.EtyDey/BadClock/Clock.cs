using System;

namespace BadClock
{
    public class Clock
    {
        public static double NextAgreement(string trueTime, string skewTime, int hourlyGain) {
            //split hours, minutes and seconds
            string[] trueTimeStrings = trueTime.Split(":");
            string[] skewTimeStrings = skewTime.Split(":");


            int seconds = 0;
            seconds = ((Convert.ToInt32(trueTimeStrings[0]) - Convert.ToInt32(skewTimeStrings[0])) * 60 * 60) + // hours to seconds
                ((Convert.ToInt32(trueTimeStrings[1]) - Convert.ToInt32(skewTimeStrings[1])) * 60) + // minutes to seconds
                (Convert.ToInt32(trueTimeStrings[2]) - Convert.ToInt32(skewTimeStrings[2])); //seconds

            if (seconds == 0) return 0.0;
            if ((seconds > 0) && (hourlyGain < 0))
            {
                // Clock is behind, and losing.
                seconds -= 12 * 60 * 60;  // Move skewTime ahead 12 hours
            }
            else if ((seconds < 0) && (hourlyGain > 0))
            {
                // Clock is ahead, and gaining
                seconds += 12 * 60 * 60;  // Move skewTime behind 12 hours
            }

            double time = Convert.ToDouble(seconds) / Convert.ToDouble(hourlyGain);
            return time;           
        }        
    }
}