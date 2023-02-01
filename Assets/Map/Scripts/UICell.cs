using UnityEngine;
using UnityEngine.UI;

namespace Shoping
{
    public class UICell : MonoBehaviour, IUICellable
    {
        [SerializeField] private Sprite _conveyor;
        [SerializeField] private Sprite _creator;
        [SerializeField] private Sprite _saller;
        [SerializeField] private Sprite _default;

        public Button Button;
        public Image Image;

        public void SetCell(ItemCellType type)
        {
            switch (type)
            {
                case ItemCellType.Conveyor:
                    Image.sprite = _conveyor;
                    break;
                case ItemCellType.Creator:
                    Image.sprite = _creator;
                    break;
                case ItemCellType.Saller:
                    Image.sprite = _saller;
                    break;
                case ItemCellType.RotateLeft:
                    break;
                case ItemCellType.RotateRight:
                    break;
                case ItemCellType.Switch:
                    break;
                case ItemCellType.Destroy:
                    Image.sprite = _default;
                    break;
                default:
                    break;
            }
        }
    }
}