using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shoping
{
    public class Button : MonoBehaviour, IButton
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Item _item;
        [SerializeField] private float _cooldownPressed = 0.5f;

        public event UnityAction<Wallet> OnPressed;

        private IEnumerator _pressed = null;
        private Wallet _targetWallet;

        private void Start()
        {
            _targetWallet = FindObjectOfType<Wallet>();
            _slider.maxValue = _cooldownPressed;
            _slider.value = _slider.minValue;
        }

        private void OnTriggerEnter(Collider other)
        {
            TryStartPressed();
        }

        private void OnTriggerExit(Collider other)
        {
            TryStopPressed();
        }

        private void TryStartPressed()
        {
            var itemInfo = _item.GetItemInfo();
            if (_pressed != null || itemInfo.PriceBuy >= _targetWallet.GetBalance(itemInfo.CurrencyType) || itemInfo.Upgrades[_item.GetUpgradeLevel()].Price >= _targetWallet.GetBalance(itemInfo.CurrencyType)) return;
            // тут не работать код ^

            _pressed = PressedCooldown(_cooldownPressed);
            StartCoroutine(_pressed);
        }

        private void TryStopPressed()
        {
            if (_pressed == null) return;

            StopCoroutine(_pressed);
            _pressed = null;
            _slider.value = _slider.minValue;
        }

        private IEnumerator PressedCooldown(float duration)
        {
            while (true)
            {
                float currentPosition = 0;
                while (currentPosition <= duration)
                {
                    yield return new WaitForSeconds(Time.deltaTime);
                    currentPosition += Time.deltaTime;
                    _slider.value = Mathf.Clamp(currentPosition, 0, duration);
                }
                OnPressed?.Invoke(_targetWallet);
            }
        }
    }
}