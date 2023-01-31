using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shoping.A
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _elements = new List<GameObject>();
        [SerializeField] private ItemData _itemData;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private bool _awakeUpgrade;

        private int _currentLevel = 0;
        private float _currenutIncomeAmount = 0;

        #region UnityMethod

        private void Start()
        {
            if (!_awakeUpgrade)
            {
                foreach (var element in _elements)
                {
                    element.SetActive(false);
                }
            }
            else
            {
                _currentLevel++;
                DisplayVisualItem(_currentLevel);
            }
            DisplayPrice(_itemData.Upgrades[_currentLevel].Price, _itemData.Upgrades[_currentLevel].Icon);
        }

        private void OnEnable()
        {
            _itemData.ButtonUpgrade.OnPress += OnPressedUpgrade;
            //_itemData.ButtonIncome.OnPress += OnPressedIncome;
        }

        private void OnDisable()
        {
            _itemData.ButtonUpgrade.OnPress -= OnPressedUpgrade;
            //_itemData.ButtonIncome.OnPress -= OnPressedIncome;
        }

        #endregion

        #region Private

        private void OnPressedUpgrade()
        {
            TryBuy();
        }

        private void TryBuy()
        {
            switch (_itemData.Upgrades[_currentLevel].CurrencyType)
            {
                case CurrencyType.Coin:
                    if (_wallet.GetBalance(CurrencyType.Coin) >= _itemData.Upgrades[_currentLevel].Price)
                    {
                        _wallet.CurrencyTransfer(CurrencyType.Coin, _itemData.Upgrades[_currentLevel].Price);
                        if (_itemData.Upgrades.Count != _currentLevel + 1)
                        {
                            DisplayPrice(_itemData.Upgrades[_currentLevel + 1].Price, _itemData.Upgrades[_currentLevel + 1].Icon);
                        }
                        DisplayVisualItem(_currentLevel);

                        _currentLevel++;
                    }
                    break;
                case CurrencyType.Ads:
                    // Show Ads
                    DisplayVisualItem(_currentLevel);
                    if (_itemData.Upgrades.Count != _currentLevel)
                    {
                        DisplayPrice(_itemData.Upgrades[_currentLevel].Price, _itemData.Upgrades[_currentLevel].Icon);
                    }

                    _currentLevel++;
                    break;
                default:
                    break;
            }

            if (_currentLevel >= _itemData.Upgrades.Count)
            {
                _itemData.ButtonUpgrade.gameObject.SetActive(false);
            }
        }

        private void DisplayPrice(float value, Sprite icon, string format = null)
        {
            if (CheckTrueFormat(format))
            {
                format = "F0";
                if (value <= 100)
                    format = "F1";
            }

            _itemData.PriceText.text = value.ToString(format);
            _itemData.PriceIconImage.sprite = icon;
        }

        private void DisplayVisualItem(int level)
        {
            var actives = _itemData.Upgrades[level].ActiveObjects;
            var inactives = _itemData.Upgrades[level].InactiveObjects;

            for (int i = 0; i < actives.Count; i++)
                actives[i].SetActive(true);
            for (int i = 0; i < inactives.Count; i++)
                inactives[i].SetActive(false);
        }

        private bool CheckTrueFormat(string format)
        {
            if (format != "F0" || format != "F1" || format != "F2")
                return false;

            return true;
        }

        #endregion

        #region Public

        #endregion
    }
}