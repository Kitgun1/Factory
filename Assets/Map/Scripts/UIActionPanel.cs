using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shoping
{
    public class UIActionPanel : MonoBehaviour
    {
        [SerializeField] private ButtonGroupData _buttonGroupEmpty;
        [SerializeField] private ButtonGroupData _buttonGroupFill;

        [SerializeField] private UIMap _uIMap;

        private UIItemData _selectedCell;

        private void Start()
        {
            ClearSelect();
        }

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
                SwitchGroup(_buttonGroupEmpty.Buttons, false);
                SwitchGroup(_buttonGroupFill.Buttons, true);
            }
            else if (cell.CellState == CellStateType.Empty)
            {
                SwitchGroup(_buttonGroupEmpty.Buttons, true);
                SwitchGroup(_buttonGroupFill.Buttons, false);
            }
            else
            {
                SwitchGroup(_buttonGroupEmpty.Buttons, false);
                SwitchGroup(_buttonGroupFill.Buttons, false);
            }
            _selectedCell = cell;
        }

        public void Insert(int index)
        {
            if (index <= 4)
            {
                _selectedCell.UICell.SetCell((ItemCellType)index);
                _selectedCell.CellState = CellStateType.Fill;
                SwitchGroup(_buttonGroupEmpty.Buttons, false);
                SwitchGroup(_buttonGroupFill.Buttons, true);
            }
        }

        private void SwitchGroup(List<Button> buttons, bool value)
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].interactable = value;
        }

        private void ClearSelect()
        {
            SwitchGroup(_buttonGroupEmpty.Buttons, false);
            SwitchGroup(_buttonGroupFill.Buttons, false);
        }
    }
}