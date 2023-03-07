using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(BoxCollider))]
    public class Destroyer : Structure
    {
        private void OnEnable()
        {
            Init();
            ProductInside += OnProductGet;
        }

        private void OnDisable()
        {
            ProductInside -= OnProductGet;
        }

        protected virtual void OnProductGet(Product product)
        {
            Destroy(product.gameObject);
        }

        private void OnValidate()
        {
            SpeedTickModifers = LimitList(SpeedTickModifers, MaxLevel);
        }
    }
}