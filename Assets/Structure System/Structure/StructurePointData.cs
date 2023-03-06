using KiMath;
using System;
using UnityEngine;

namespace Factory
{
    [Serializable]
    public struct StructurePointData
    {
        [HideInInspector] public Product Product;
        [HideInInspector] public Vector2Int Axis;
        public PointState State;
        public PriorityType Priority;

        public void SetAxis(Direction direction) => Axis = direction.GetAxis();

        public StructurePointData(Direction direction)
        {
            Product = null;
            Axis = direction.GetAxis();
            State = PointState.Close;
            Priority = PriorityType.Main;
        }

        public StructurePointData(PointState state, PriorityType outputType, Direction direction)
        {
            Product = null;
            Axis = direction.GetAxis();
            State = state;
            Priority = outputType;
        }
    }
}