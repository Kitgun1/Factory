using UnityEngine;

namespace Factory
{
    public class VisualUpgrader : Interacter
    {
        private void OnEnable()
        {
            Init();
            ProductGet += OnProductGet;
        }

        private void OnDisable()
        {
            ProductGet -= OnProductGet;
        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
        }

        protected override void Action(Product product)
        {
            if(product is IUpgradeable upgradeable)
                upgradeable.TryUpgrade();
        }
    }
}