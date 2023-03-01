using UnityEngine;

namespace Factory
{
    public abstract class Product : MonoBehaviour, IUpgradeable
    {
        public int Level { get; private set; }
        public bool Cloned { get; private set; }
        public ProductTemplate Templtate { get; private set; }

        public virtual void Init(ProductTemplate template, bool cloned)
        {
            Templtate = template;
            Cloned = cloned;
            Level = 0;

            UpdateInfo();
        }

        public abstract long Price();

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