using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Product : MonoBehaviour
    {
        public int Level { get; private set; }
        public ProductTemplate Templtate { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public virtual void Init(ProductTemplate template)
        {
            Rigidbody = GetComponent<Rigidbody>();

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