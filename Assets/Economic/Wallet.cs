using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class Wallet : MonoBehaviour, IWallet
    {
        [SerializeField] private List<CurrencyData> _currencies;

        private void Start()
        {
            CurrencySetDefaultAll();
        }

        public float GetBalance(CurrencyType type)
        {
            foreach (var currency in _currencies)
                if (currency.CurrencyType == type) return currency.BalanceValue;

            return 0;
        }

        public void CurrencySet(CurrencyType type, float value)
        {
            for (int i = 0; i < _currencies.Count; i++)
            {
                if (_currencies[i].CurrencyType == type)
                {
                    var temp = _currencies[i];
                    temp.BalanceValue = value;
                    _currencies[i] = temp;
                }
            }
            DisplayCurrency(type);
        }

        public void CurrencyTransfer(CurrencyType type, float value)
        {
            for (int i = 0; i < _currencies.Count; i++)
            {
                if (_currencies[i].CurrencyType == type)
                {
                    var temp = _currencies[i];
                    temp.BalanceValue -= value;
                    _currencies[i] = temp;
                }
            }
            DisplayCurrency(type);
        }

        public void CurrencyTransfer(CurrencyType type, float value, out float walletBalance)
        {
            walletBalance = 0;

            for (int i = 0; i < _currencies.Count; i++)
            {
                if (_currencies[i].CurrencyType == type)
                {
                    var temp = _currencies[i];
                    temp.BalanceValue -= value;
                    _currencies[i] = temp;
                    walletBalance = _currencies[i].BalanceValue;
                }
            }
            DisplayCurrency(type);
        }

        public void CurrencySetDefault(CurrencyType type)
        {
            for (int i = 0; i < _currencies.Count; i++)
            {
                if (_currencies[i].CurrencyType == type)
                {
                    var temp = _currencies[i];
                    temp.BalanceValue = temp.BalanceDefaultValue;
                    _currencies[i] = temp;
                }
            }
            DisplayCurrency(type);
        }

        public void CurrencySetAll(float value)
        {
            for (int i = 0; i < _currencies.Count; i++)
            {
                var temp = _currencies[i];
                temp.BalanceValue = value;
                _currencies[i] = temp;
            }
            DisplayCurrencyAll();
        }

        public void CurrencySetDefaultAll()
        {
            for (int i = 0; i < _currencies.Count; i++)
            {
                var temp = _currencies[i];
                temp.BalanceValue = _currencies[i].BalanceDefaultValue;
                _currencies[i] = temp;
            }
            DisplayCurrencyAll();
        }

        private void DisplayCurrency(CurrencyType type)
        {
            for (int i = 0; i < _currencies.Count; i++)
            {
                if (_currencies[i].CurrencyType == type)
                {
                    _currencies[i].BalanceText.text = _currencies[i].BalanceValue.ToString("F0");
                    _currencies[i].Image.sprite = _currencies[i].Sprite;
                }
            }
        }

        private void DisplayCurrencyAll()
        {
            for (int i = 0; i < _currencies.Count; i++)
            {
                _currencies[i].BalanceText.text = _currencies[i].BalanceValue.ToString("F0");
                _currencies[i].Image.sprite = _currencies[i].Sprite;
            }
        }
    }
}