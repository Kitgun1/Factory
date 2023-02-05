using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shoping
{
    public class ActionPanel : MonoBehaviour
    {
        [SerializeField] private List<Button> _forEmpty;
        [SerializeField] private List<Button> _forFill;
        [SerializeField] private MapManage _mapManager;

        private ConnectionData<GameObject, Cell> _currentCellConnection = new ConnectionData<GameObject, Cell>(null, null);

        public void SetCurrentCell(ConnectionData<GameObject, Cell> connection) => _currentCellConnection = connection;

        public List<Button> GetButtons(ItemType type)
        {
            if (type == ItemType.Empty)
                return _forEmpty;

            return _forFill;
        }

        public void SetInteractables(ItemType type, bool value = true)
        {
            if (type == ItemType.Empty)
            {
                foreach (var button in _forEmpty)
                    button.interactable = value;
                foreach (var button in _forFill)
                    button.interactable = !value;
                return;
            }

            foreach (var button in _forEmpty)
                button.interactable = !value;
            foreach (var button in _forFill)
                button.interactable = value;
        }

        public void Insert(string name)
        {
            CellInfoData info = _mapManager.GetCellInfo(name);
            _currentCellConnection.TValue2.SetCell(info.Icon, info.GameObject);
        }
    }
}