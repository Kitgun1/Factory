using UnityEngine;

namespace Factory
{
	public class PlayerAnimation : MonoBehaviour
	{
		private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetAnimationFloat(AnimationParamType type, float value)
        {
            _animator.SetFloat($"{type}", value);
        }
    }
}