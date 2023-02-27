using UnityEngine;

namespace Factory
{
    public struct SliderData<T> where T : struct
    {
        public T MinValue;
        public T MaxValue;
        public T Value;

        public SliderData(T minValue, T maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Value = minValue;
        }

        public SliderData(T minValue, T maxValue, T value)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Value = value;
        }
    }
}