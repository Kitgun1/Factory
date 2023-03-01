using System;
using UnityEngine;

namespace Factory
{
    [Serializable]
    public struct StrucurePointsAxisData
    {
        [Header(nameof(Up))]
        public StructurePoint PointUp;
        public StructurePointState Up;

        [Space(2), Header(nameof(Right))]
        public StructurePoint PointRight;
        public StructurePointState Right;

        [Space(2), Header(nameof(Down))]
        public StructurePoint PointDown;
        public StructurePointState Down;

        [Space(2), Header(nameof(Left))]
        public StructurePoint PointLeft;
        public StructurePointState Left;

    }
}