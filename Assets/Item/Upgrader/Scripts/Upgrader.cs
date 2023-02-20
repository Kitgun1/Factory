using UnityEngine;

namespace Factory
{
	public class Upgrader : Item
	{
        private Product _levelInfo;

        public void Init(Product info)
        {
            _levelInfo = info;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.TryGetComponent(out Product product))
        //        UpgrateProduct(product);
        //}

        //private void UpgrateProduct(Product product)
        //{

        //}
    }
}