using UnityEngine;

namespace Factory
{
    public class VisualUpgrader : Interacter
    {
        protected override void Action(Product product)
        {
            if(product is IUpgradeable upgradeable)
                upgradeable.TryUpgrade();
        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
        }
    }
}