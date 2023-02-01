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
                    int tempX = x;
                    int tempY = y;
                    var UICell = Instantiate(_uIItem, _parent);
                    _data[tempX, tempY].UICell = UICell;
                    _data[tempX, tempY].CellState = CellStateType.Empty;
                    _data[tempX, tempY].UICell.Button.onClick.RemoveAllListeners();
                    _data[tempX, tempY].UICell.Button.onClick.AddListener(() => OnSelectCell?.Invoke(_data[tempX, tempY]));
                }
            }
        }
    }
}