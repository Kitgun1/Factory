using System;
using UnityEngine;

namespace KiMath
{
    public static class MathK
    {
        #region Rounding

        public static int RoundToInt(this float value)
        {
            if (value >= 0f && Math.Abs(value % 1) < 0.5f)
                value -= value % 1;
            else if (value >= 0f)
                value += 1 - value % 1;
            else if (value < 0f && Math.Abs(value % 1) > 0.5f)
                value -= 1 + value % 1;
            else
                value -= value % 1;

            return (int)value;
        }

        public static Vector2Int RoundToVector2Int(this Vector2 value)
        {
            Vector2Int vectorInt = new Vector2Int();
            vectorInt.x = value.x.RoundToInt();
            vectorInt.y = value.y.RoundToInt();

            return vectorInt;
        }

        public static Vector3Int RoundToVector3Int(this Vector3 value)
        {
            Vector3Int vectorInt = new Vector3Int();
            vectorInt.x = value.x.RoundToInt();
            vectorInt.y = value.y.RoundToInt();
            vectorInt.z = value.z.RoundToInt();

            return vectorInt;
        }

        #endregion

        #region FloatToString

        public static string ToString(this float value)
        {
            if (value % 10 > 0)
                return value.ToString("F2");
            else if (value % 100 > 0)
                return value.ToString("F1");
            else return value.ToString("F0");
        }

        #endregion
    }
}