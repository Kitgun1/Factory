using UnityEngine;

namespace Factory
{
    public class ProductTypeFilter : Filter
    {
        [SerializeField] ProductTypeFilterData _data;

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