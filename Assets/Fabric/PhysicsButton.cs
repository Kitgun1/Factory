using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shoping
{
    [RequireComponent(typeof(BoxCollider))]
    public class PhysicsButton : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [Layer, SerializeField] private int _layer;

        private IEnumerator _isPressed = null;

        public event UnityAction OnPress;
        public event UnityAction<bool> OnInside;

        private void Awake()
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != _layer || _isPressed != null) return;

            if (_slider != null)
            {
                StartPress();
            }
            else
            {
                OnInside?.Invoke(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != _layer || _isPressed == null) return;

            if (_slider != null)
            {
                StopPress();
            }
            else
            {
                OnInside?.Invoke(false);
            }
        }

        private IEnumerator LoadingPress()
        {
            while (_slider.value < _slider.maxValue)
            {
                yield return new WaitForFixedUpdate();
                _slider.value += Time.fixedDeltaTime;
            }
            OnPress?.Invoke();
            StartPress();
        }

        private void StartPress()
        {
            _slider.value = _slider.minValue;

            if (_isPressed != null)
                StopPress();

            if (!gameObject.activeSelf) return;
            _isPressed = LoadingPress();
            StartCoroutine(_isPressed);
        }

        private void StopPress()
        {
            StopCoroutine(_isPressed);
            _isPressed = null;
            _slider.value = _slider.minValue;
        }
    }
}