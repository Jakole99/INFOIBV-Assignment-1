using System;

namespace INFOIBV.Extensions
{
    public static class MathExtensions
    {
        /// <summary>
        /// Clamps a value between a minimum and a maximum
        /// </summary>
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                value = min;

            if (value.CompareTo(max) > 0)
                value = max;

            return value;
        }
    }
}