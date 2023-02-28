using Factory;
using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Jobs;

namespace KiMath
{
    public static class MathK
    {
        #region Rounding

        public static int RoundFloatToInt(this float value)
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

        public static Vector2Int RoundVector2ToVector2Int(this Vector2 value)
        {
            Vector2Int vectorInt = new Vector2Int();
            vectorInt.x = value.x.RoundFloatToInt();
            vectorInt.y = value.y.RoundFloatToInt();

            return vectorInt;
        }

        public static Vector3Int RoundVector3ToVector3Int(this Vector3 value)
        {
            Vector3Int vectorInt = new Vector3Int();
            vectorInt.x = value.x.RoundFloatToInt();
            vectorInt.y = value.y.RoundFloatToInt();
            vectorInt.z = value.z.RoundFloatToInt();

            return vectorInt;
        }

        #endregion

        #region Spliting 

        public static float[] SplitWay(float width, float size, int splitCount, SplitType splitType = SplitType.Center)
        {
            float indent = width / splitCount;
            float[] positions = new float[splitCount];

            switch (splitType)
            {
                case SplitType.Center:
                    SetPosition(ref positions, indent, indent / 2);
                    break;
                case SplitType.RigthSide:
                    SetPosition(ref positions, indent, size / 2);
                    break;
                case SplitType.LeftSide:
                    SetPosition(ref positions, indent, -(size / 2));
                    break;
            }

            return positions;
        }

        private static void SetPosition(ref float[] positions, float indent, float offset)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] += indent * i;
                positions[i] += offset;
            }
        }

        #endregion

        #region Borders

        public static bool PointInBorders(this Vector2 point, FloatRange xRange, FloatRange yRange)
        {
            bool xInBorders = NumberInBorders(point.x, xRange);
            bool yInBorders = NumberInBorders(point.y, yRange);

            return xInBorders && yInBorders;
        }

        public static bool PointInBorders(this Vector2Int point, IntRange xRange, IntRange yRange)
        {
            bool xInBorders = NumberInBorders(point.x, xRange);
            bool yInBorders = NumberInBorders(point.y, yRange);

            return xInBorders && yInBorders;
        }

        private static bool NumberInBorders(float number, FloatRange range) => number < range.Max && number > range.Min;

        private static bool NumberInBorders(int number, IntRange range) => number < range.Max && number > range.Min;

        #endregion
    }
}