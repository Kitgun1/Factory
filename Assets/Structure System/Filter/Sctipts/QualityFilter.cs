using UnityEngine;

namespace Factory
{
    public class QualityFilter : Filter
    {
        [SerializeField] QualityFilterData _data;

        private void OnEnable()
        {
            Init();
            ProductInside += OnProductGet;
        }

        private void OnDisable()
        {
            ProductInside -= OnProductGet;
        }

        protected void InitFilter(QualityFilterData data)
        {
            _data = data;

            Init();
        }

        protected override bool FilterProduct(Product product)
        {
            if (product is QualityProduct qualityProduct)
            {
                switch (_data.Sign)
                {
                    case Sign.More:
                        if (qualityProduct.Quality > _data.QualityPencent)
                            return true;
                        else
                            return false;
                    case Sign.Less:
                        if (qualityProduct.Quality < _data.QualityPencent)
                            return true;
                        else
                            return false;
                }
            }

            return false;
        }
    }
}