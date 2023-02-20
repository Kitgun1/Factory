using UnityEngine;
using KiMath;

namespace Factory
{
    public class Map
    {
        private Cell[,] _cells;

        public Map()
        {
            Init();
        }

        public Map(int width = 5, int height = 5)
        {
            Init(width, height);
        }

        public Map(Item itemDefault, int width = 5, int height = 5)
        {
            Init(itemDefault, width, height);
        }

        private void Init(int width = 5, int height = 5)
        {
            _cells = new Cell[width, height];
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                {
                    _cells[x, y] = new Cell();
                }
            }
        }

        private void Init(Item itemDefault, int width = 5, int height = 5)
        {
            _cells = new Cell[width, height];
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                {
                    _cells[x, y] = new Cell(itemDefault);
                }
            }
        }

        public Cell GetNearCell(Vector2 position, Vector2Int sizeMap, out Vector2 worldPositionCell)
        {
            worldPositionCell = (Vector2)position.RoundVector2ToVector2Int();

            Vector2Int positionCell = worldPositionCell.RoundVector2ToVector2Int() + ((sizeMap - Vector2.one * 2) / 2).RoundVector2ToVector2Int();

            if (positionCell.x > _cells.GetLength(0) - 1 || positionCell.x < 0)
                positionCell.x = -1;
            if (positionCell.y > _cells.GetLength(1) - 1 || positionCell.y < 0)
                positionCell.y = -1;

            if (positionCell.x == -1 || positionCell.y == -1) return null;
            Debug.Log($"{_cells[positionCell.x, positionCell.y]} | {positionCell.x} ~ {positionCell.y}");
            return _cells[positionCell.x, positionCell.y];
        }
    }
}