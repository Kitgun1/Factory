using UnityEngine;

namespace Factory.KiMath.Structures
{
    public struct LetterValue
    {
        public string Name { get; private set; }
        public float Value { get; private set; }

        public LetterValue(float value, string name)
        {
            Name = name;
            Value = value;
        }
    }
}