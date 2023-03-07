using KiMath;
using NaughtyAttributes;
using System;
using UnityEngine;

namespace Factory
{
    [Serializable]
    public struct StructurePointData
    {
        public PointState State;
        public PriorityType Priority;
        [HideInInspector] public Product Product;
        public Direction Axis;

        public void SetAxis(Direction direction) => Axis = direction;

        public StructurePointData(Direction direction)
        {
            Product = null;
            Axis = direction;
            State = PointState.Close;
            Priority = PriorityType.Main;
        }

        public StructurePointData(PointState state, PriorityType outputType, Direction direction)
        {
            Product = null;
            Axis = direction;
            State = state;
            Priority = outputType;
        }
    }
}