using UnityEngine;

namespace Shoping
{
    public class Map : IInsertable, ITurnable, IDestroyable
    {
        public CellData[,] CellMatrix { get; private set; }

        public Map(int width, int height)
        {
            CellMatrix = new CellData[width, height];
        }

        public void InsertItem(Item item, int x, int y, float rotationY = 0, Transform parent = null)
        {
            Vector2Int positionMap = ClampBorders(x, y);
            if (CheckForStatus(positionMap.x, positionMap.y) == CellStateType.Fill)
            {
                Debug.Log($"Cell {positionMap} - NotEmpty!");
                return;
            }

            Vector3 position = new Vector3(x, 0f, y);
            Quaternion rotation = Quaternion.Euler(0f, rotationY, 0f);

            CellMatrix[positionMap.x, positionMap.y].Item = Object.Instantiate(item, position, rotation, parent);
        }

        public void TurnItem(int x, int y, float newRotationY)
        {
            Vector2Int positionMap = ClampBorders(x, y);
            if (CheckForStatus(x, y) != CellStateType.Fill) return;

            Quaternion rotation = Quaternion.Euler(0f, newRotationY, 0f);
            CellMatrix[positionMap.x, positionMap.y].Item.transform.rotation = rotation;
        }

        public void DestroyItem(int x, int y)
        {
            Vector2Int positionMap = ClampBorders(x, y);
            if (CheckForStatus(x, y) == CellStateType.Fill)
            {
                Debug.Log($"Cell {positionMap} - Empty!");
                return;
            }

            Object.Destroy(CellMatrix[positionMap.x, positionMap.y].Item);
            CellMatrix[positionMap.x, positionMap.y].Item = null;
        }

        private Vector2Int ClampBorders(int x, int y)
        {
            return new Vector2Int(Mathf.Clamp(x, 0, CellMatrix.GetLength(0)), Mathf.Clamp(y, 0, CellMatrix.GetLength(1)));
        }

        private CellStateType CheckForStatus(int x, int y)
        {
            if (CellMatrix[x, y].Item != null)
                return CellStateType.Fill;
            return CellStateType.Empty;
        }
    }
}