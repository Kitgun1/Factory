using UnityEngine;

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

        public Cell GetNearCell(Vector2 position, float sizeCell, Vector2Int sizeMap, out Vector2Int positionCell)
        {
            //Vector2 positionCell = new Vector2 (position - new Vector2(sizeMap.x, sizeMap.y)) / 2;
            //Debug.Log(positionCell);
            positionCell = Vector2Int.zero;
            return default;
        }
    }
}