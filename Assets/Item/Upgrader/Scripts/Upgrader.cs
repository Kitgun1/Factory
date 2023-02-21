using UnityEngine;

namespace Factory
{
	public class Upgrader : Item
	{
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Product product))
                product.TryUpgrade();
        }
    }
}