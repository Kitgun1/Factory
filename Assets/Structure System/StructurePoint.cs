using UnityEngine;

namespace Factory
{
    public class StructurePoint
    {
        public Product Product;
        public StructurePointState State;
        public Vector2Int Axis;

        public StructurePoint(Vector2Int axis)
        {
            State = StructurePointState.Close;
            Axis = axis;
        }

        public StructurePoint(StructurePointState state, Vector2Int axis)
        {
            State = state;
            Axis = axis;
        }
    }
}