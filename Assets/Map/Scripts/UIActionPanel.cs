using UnityEngine;

namespace Shoping
{
    public class UIActionPanel : MonoBehaviour
    {
        [SerializeField] private ButtonGroupData _buttonGroupBuilder;
        [SerializeField] private ButtonGroupData _buttonGroupRotate;

        [SerializeField] private UIMap _uIMap;

        private void OnEnable()
        {
            _uIMap.OnSelectCell += OnSelectedCell;
        }

        private void OnDisable()
        {
            _uIMap.OnSelectCell -= OnSelectedCell;
        }

        public void OnSelectedCell(UIItemData cell)
        {
            if (cell.CellState == CellStateType.Fill)
            {
                for (int i = 0; i < _buttonGroupBuilder.Buttons.Count; i++)
                {
                    _buttonGroupBuilder.Buttons[i].interactable = true;

                }
                _buttonGroupRotate.GroupParent.SetActive(true);
            }
            else if (cell.CellState == CellStateType.Empty)
            {
                _buttonGroupBuilder.GroupParent.SetActive(true);
                _buttonGroupRotate.GroupParent.SetActive(false);
            }
        }
    }
}