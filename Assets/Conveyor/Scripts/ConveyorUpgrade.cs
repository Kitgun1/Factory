using Shoping.A;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shoping
{

    public class ConveyorUpgrade : MonoBehaviour, IUpgrader
    {
        [Header("Links")]
        [SerializeField] private Wallet _wallet;
        [SerializeField] private PhysicsButton _physicsButton;
        [SerializeField] private Image _priceIcon;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _modiferText;
        [SerializeField] private TMP_Text _levelText;

        [Space(5f), Header("Property")]
        [SerializeField] private List<UpgradeData> _upgradeData = new List<UpgradeData>();
        [SerializeField] private bool _awakeUpgrade = false;

        private int _currentLevel = 0;

        public bool TryUpgrade()
        {
            if (_wallet.GetBalance(_upgradeData[_currentLevel].CurrencyType) >= _upgradeData[_currentLevel].UpgradeAmount)
            {
                _wallet.CurrencyTransfer(_upgradeData[_currentLevel].CurrencyType, _upgradeData[_currentLevel].UpgradeAmount);
                if (_upgradeData.Count != _currentLevel + 1)
                {
                    //DisplayPrice(_itemData.Upgrades[_currentLevel + 1].Price, _itemData.Upgrades[_currentLevel + 1].Icon);
                }
                //DisplayVisualItem(_currentLevel);
                _currentLevel++;
            }

            if (_currentLevel >= _upgradeData.Count)
            {
                _physicsButton.gameObject.SetActive(false);
            }

            return false;
        }
    }
}