using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interacter : Structure
    {
        [SerializeField] protected List<float> MaxProductCount;
        [SerializeField] private float _enterDuration;
        [SerializeField] private Transform _holdPosition;
        [SerializeField] private Transform _relesePosition;
        [SerializeField] private Transform _enterPosition;

        private List<Product> _productsInside = new List<Product>();
        private List<Product> _productsOutside = new List<Product>();

        private bool Full => _productsInside.Count > MaxProductCount[Level];

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Product product))
            {
                if (Full == false)
                    product.transform.DOMove(_enterPosition.position, _enterDuration);
                else
                    _productsOutside.Add(product);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Product product))
            {
                if (_productsOutside.Contains(product))
                    _productsOutside.Remove(product);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Product product))
                AddProduct(product);
        }

        private void AddProduct(Product product)
        {
            product.transform.position = _holdPosition.position;
            product.gameObject.SetActive(false);
            _productsInside.Add(product);
            StartCoroutine(ServeProduct(product));
        }

        private IEnumerator ServeProduct(Product product)
        {
            yield return new WaitForSeconds(Modifer[Level]);
            Action(product);
            RelizeProduct(product);
        }

        private void RelizeProduct(Product product)
        {
            _productsInside.Remove(product);
            product.transform.position = _relesePosition.position;
            product.gameObject.SetActive(true);
            OnRelizeProduct();
        }

        private void OnRelizeProduct()
        {
            if (_productsOutside.Count > 0)
                AddProduct(_productsOutside.First());
        }

        public override void LimitModifer()
        {
            base.LimitModifer();

            if (MaxProductCount.Count > MaxLevel)
            {
                MaxProductCount.RemoveRange(MaxLevel, MaxProductCount.Count - MaxLevel);
            }
            else if (MaxProductCount.Count < MaxLevel)
            {
                List<float> tempLevels = new List<float>(MaxProductCount);
                for (int i = 0; i < MaxLevel - MaxProductCount.Count; i++)
                {
                    tempLevels.Add(0f);
                }
                MaxProductCount = new List<float>(tempLevels);
            }
        }

        protected abstract void Action(Product product);
    }
}