using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
	public class TickManage : MonoBehaviour
	{
		public static TickManage Instance { get; private set; }

		public event UnityAction HundredthTick;
		public event UnityAction SecondTick;
		public event UnityAction TenSecondTick;
		public event UnityAction HundredSecondTick;

		private const float _hundredth = 0.01f;
		private const float _second = 1f;
		private const float _tenSecond = 10f;
		private const float _hundredSecond = 100f;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
			StartCoroutine(Timer(_hundredth, HundredthTick));
			StartCoroutine(Timer(_second, SecondTick));
			StartCoroutine(Timer(_tenSecond, TenSecondTick));
			StartCoroutine(Timer(_hundredSecond, HundredSecondTick));
        }

		private void OnDisable()
        {
            StopCoroutine(Timer(_hundredth, HundredthTick));
            StopCoroutine(Timer(_second, SecondTick));
            StopCoroutine(Timer(_tenSecond, TenSecondTick));
            StopCoroutine(Timer(_hundredSecond, HundredSecondTick));
        }

        private IEnumerator Timer(float time, UnityAction action)
		{
			while(true)
			{
				yield return new WaitForSeconds(time);
				action?.Invoke();
			}
		}
    }
}