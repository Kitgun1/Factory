using UnityEngine;

namespace Factory
{
	public class Upgrader : Item
	{
        private ProductTemplate _levelInfo;

        public void Init(ProductTemplate info)
        {
            _levelInfo = info;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ProductTemplate product))
                UpgrateProduct(product);
        }

        private void UpgrateProduct(ProductTemplate product)
        {

        }
    }
}