using UnityEngine;

namespace Factory
{
	public interface IRotate
	{
        public void Rotate(Transform transform, Vector2 direction);
    }
}