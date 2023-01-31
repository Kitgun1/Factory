using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shoping
{
    public class UIMap : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private GridLayoutGroup _layout;
        [SerializeField] private UICell _uIItem;

        private UIItemData[,] _data;

        public event UnityAction<UIItemData> OnSelectCell;

        public void SetMap(int sizeX, int sizeY)
        {
            _data = new UIItemData[sizeX, sizeY];
            InitMap(sizeX, sizeY);
        }

        private void InitMap(int sizeX, int sizeY)
        {
            _layout.constraintCount = sizeX;
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    var UICell = Instantiate(_uIItem, _parent);
                    _data[x, y].UICell = UICell;
                    _data[x, y].CellState = CellStateType.Empty;
                    OnSelectCell?.Invoke(_data[x, y]);
                }
            }
        }
    }
}