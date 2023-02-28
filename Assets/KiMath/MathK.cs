using Factory;
using System;
using System.Drawing;
using System.Numerics;
using UnityEngine;
using UnityEngine.Jobs;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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

        public static Vector2Int RoundToVector2Int(this UnityEngine.Vector2 value)
        {
            Vector2Int vectorInt = new Vector2Int();
            vectorInt.x = value.x.RoundToInt();
            vectorInt.y = value.y.RoundToInt();

            return vectorInt;
        }

        public static Vector3Int RoundToVector3Int(this UnityEngine.Vector3 value)
        {
            Vector3Int vectorInt = new Vector3Int();
            vectorInt.x = value.x.RoundToInt();
            vectorInt.y = value.y.RoundToInt();
            vectorInt.z = value.z.RoundToInt();

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

        #region MoneyCoverter

        private const string Thousand = "K";
        private const string Million = "M";
        private const string Billion = "B";
        private const string Trillion = "T";
        private const int Indent = 1000;

        public static string ConvertMoney(this BigInteger value)
        {
            int tryCount = 0;
            float newValue = (float)FindMagnitude(value, ref tryCount);
            string postfix;

            switch (tryCount)
            {
                case 0:
                    postfix = Thousand;
                    break;
                case 1:
                    postfix = Million;
                    break;
                case 2:
                    postfix = Billion;
                    break;
                case 3:
                    postfix = Trillion;
                    break;
                default:
                    postfix = FindLetter(tryCount - 4);
                    break;
            }

            return newValue + postfix;
        }

        private static BigInteger FindMagnitude(BigInteger value, ref int tryCount)
        {
            if (value <= Indent)
            {
                return value;
            }
            else
            {
                tryCount++;
                return FindMagnitude(value / Indent, ref tryCount);
            }
        }

        private static string FindLetter(int degree)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (degree <= alphabet.Length)
                return alphabet[degree].ToString();
            else
                return "MANY";
        }

        #endregion
    }
}