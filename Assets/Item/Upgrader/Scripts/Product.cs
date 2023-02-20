using UnityEngine;

namespace Factory
{
	public abstract class Product : MonoBehaviour
	{
		public int Level { get; private set; }
		public ProductTemplate Templtate { get; private set; }

		public virtual void Init(ProductTemplate template)
		{
			Templtate = template;
			Level = 0;

			UpdateInfo();
		}

		public abstract void UpdateInfo();
    }
}