using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Shoping
{
    public class Button : MonoBehaviour, IButton
    {
        [SerializeField] private float _cooldownPressed = 0.5f;

        public event UnityAction<Wallet> OnPressed;

        private IEnumerator _pressed = null;
        private Wallet _targetWallet;

        private void Start()
        {
            FindObjectOfType<Wallet>();
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
            if (_pressed != null) return;

            _pressed = PressedCooldown(_cooldownPressed);
            StartCoroutine(_pressed);
        }

        private void TryStopPressed()
        {
            if (_pressed == null) return;

            StopCoroutine(_pressed);
            _pressed = null;
        }

        private IEnumerator PressedCooldown(float duration)
        {
            while (true)
            {
                yield return new WaitForSeconds(duration);
                OnPressed?.Invoke(_targetWallet);
            }
        }
    }
}