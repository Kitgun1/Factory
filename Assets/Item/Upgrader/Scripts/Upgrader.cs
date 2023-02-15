using UnityEngine;

namespace Factory
{
	public class Upgrader : Item
	{
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Product product))
                Upgrate(product);
        }

        private void Upgrate(Product product)
        {
            ProductLevelKeeper levelInfo = ProductLevelKeeper.Instance;

            product.TryUpgradeLevel(levelInfo.MaxLevel, Modifer[Level]);
            product.ChangeVisual(levelInfo.GetProductInfo(product.Level));
        }
    }
}