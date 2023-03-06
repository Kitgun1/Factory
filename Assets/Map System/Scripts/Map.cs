using UnityEngine;
using KiMath;

namespace Factory
{
    public class Map
    {
        private Structure[,] _cells;

        public Map()
        {
            Init();
        }

        public Map(int width = 5, int height = 5)
        {
            Init(width, height);
        }

        private void Init(int width = 5, int height = 5)
        {
            _cells = new Structure[width, height];
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                {
                    _cells[x, y] = null;
                }
            }
        }

        public Structure GetNearStructure(Vector2 position, Vector2Int sizeMap, out Vector2 worldPositionStructure, out Vector2Int positionStructure)
        {
            worldPositionStructure = (Vector2)position.RoundToVector2Int();

            positionStructure = worldPositionStructure.RoundToVector2Int() + ((sizeMap - Vector2.one * 2) / 2).RoundToVector2Int();

            if (positionStructure.x > _cells.GetLength(0) - 1 || positionStructure.x < 0)
                positionStructure.x = -1;
            if (positionStructure.y > _cells.GetLength(1) - 1 || positionStructure.y < 0)
                positionStructure.y = -1;

            if (positionStructure.x == -1 || positionStructure.y == -1) return null;

            return _cells[positionStructure.x, positionStructure.y];
        }

        public Structure GetStructure(int x, int y)
        {
            if (x >= _cells.GetLength(0) || x < 0 || y >= _cells.GetLength(1) || y < 0)
                return null;

            return _cells[x, y];
        }

        public void SetStructure(Structure structure, int x, int y)
        {
            _cells[x, y] = structure;
            structure.SetPosition(new Vector2Int(x, y));
        }

        public bool CheckPointInBorder(int x, int y)
        {
            if (x > _cells.GetLength(0) || x < 0 || y > _cells.GetLength(1) || y < 0)
                return false;
            return true;
        }
    }
}