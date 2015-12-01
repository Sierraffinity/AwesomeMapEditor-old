using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord
{
    static public class TimeSpanHelper
    {
        static public float FloatDivide(this TimeSpan timeSpan, TimeSpan divideWith)
        {
            return ((float)timeSpan.Ticks) / ((float)divideWith.Ticks);
        } 

        static public TimeSpan Divide(this TimeSpan timeSpan, int divideWith)
        {
            return TimeSpan.FromTicks(timeSpan.Ticks / divideWith);
        }

        static public int Divide(this TimeSpan timeSpan, TimeSpan divideWith)
        {
            return (int)(timeSpan.Ticks / divideWith.Ticks);
        }

        static public TimeSpan Multiply(this TimeSpan timeSpan, int value)
        {
            return TimeSpan.FromTicks(timeSpan.Ticks * value);
        }

        static public TimeSpan Multiply(this TimeSpan timeSpan, float value)
        {
            return TimeSpan.FromMilliseconds(timeSpan.TotalMilliseconds * value);
        }

        static public TimeSpan Part(this TimeSpan timeSpan, int toMultiply, int toDivide)
        {
            return TimeSpan.FromTicks(timeSpan.Ticks * toMultiply / toDivide);
        }

        static public TimeSpan Lerp(TimeSpan beginning, TimeSpan end, float i)
        {
            return beginning + Multiply((end - beginning) , i);
        }
    }
}
