using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(Collider))]
	public class Upgrader : Structure
	{
        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Product product))
                product.TryUpgrade();
        }
    }
}