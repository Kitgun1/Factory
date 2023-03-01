using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(BoxCollider))]
    public class Destroyer : Structure
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

        protected virtual void OnProductGet(Product product)
        {
            Destroy(product.gameObject);
        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
        }
    }
}