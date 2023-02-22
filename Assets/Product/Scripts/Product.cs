using UnityEngine;
using UnityEngine.Events;

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

        public bool TryUpgrade()
        {
            if (Level < Templtate.MaxLevel())
            {
                Level++;
                UpdateInfo();
                return true;
            }

            return false;
        }

        protected abstract void UpdateInfo();
    }
}