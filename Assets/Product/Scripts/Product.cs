using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Product : MonoBehaviour, IUpgradeable
    {
        public int Level { get; private set; }
        public bool Cloned { get; private set; }
        public ProductTemplate Templtate { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public virtual void Init(ProductTemplate template, bool cloned)
        {
            Rigidbody = GetComponent<Rigidbody>();

            Templtate = template;
            Level = 0;
            Cloned = cloned;

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