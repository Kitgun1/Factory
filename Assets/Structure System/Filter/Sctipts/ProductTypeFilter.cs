using UnityEngine;

namespace Factory
{
    public class ProductTypeFilter : Filter
    {
        [SerializeField] ProductTypeFilterData _data;

        private void OnEnable()
        {
            Init();
            ProductInside += OnProductGet;
        }

        private void OnDisable()
        {
            ProductInside -= OnProductGet;
        }

        protected void InitFilter(ProductTypeFilterData data)
        {
            _data = data;

            Init();
        }

        protected override bool FilterProduct(Product product)
        {
            return product.Templtate.Type == _data.Type;
        }
    }
}