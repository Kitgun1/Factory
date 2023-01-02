using UnityEngine;

namespace Shoping
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private ItemInfoData _itemInfoData;

        private int _currentUpgradeLevel = 0;
        private bool _isBuyed = false;

        public ItemInfoData GetItemInfo() => _itemInfoData;
        public int GetUpgradeLevel() => _currentUpgradeLevel;
        public void AddUpgradeLevel() => _currentUpgradeLevel++;

        private void Start()
        {
            DisplayItem();

            if (_isBuyed && _itemInfoData.Upgrades.Count == _currentUpgradeLevel)
            {
                _itemInfoData.Button.gameObject.SetActive(false);
                _itemInfoData.Table.SetActive(true);
            }
            else
            {
                _itemInfoData.Button.gameObject.SetActive(true);
                _itemInfoData.Table.SetActive(false);
            }
        }

        private void OnEnable()
        {
            _itemInfoData.Button.OnPressed += OnPressed;
        }

        private void OnDisable()
        {
            _itemInfoData.Button.OnPressed -= OnPressed;
        }

        public void OnPressed(Wallet wallet)
        {
            Buy(wallet);
        }

        private void Buy(Wallet wallet)
        {
            wallet.CurrencyTransfer(_itemInfoData.CurrencyType, _itemInfoData.PriceBuy);
            _isBuyed = true;
            if (_itemInfoData.Upgrades.Count == _currentUpgradeLevel)
            {
                _itemInfoData.Button.gameObject.SetActive(false);
                _itemInfoData.Table.SetActive(true);
            }
            DisplayItem();
        }

        private void DisplayItem()
        {
            if (_isBuyed)
                _itemInfoData.PriceText.text = _itemInfoData.Upgrades[_currentUpgradeLevel].Price.ToString("F0");
            else
                _itemInfoData.PriceText.text = _itemInfoData.PriceBuy.ToString("F0");
        }
    }
}