using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interacter : Structure
    {
        [SerializeField] protected List<float> MaxProductCount;
        [SerializeField] private float _moveDuration;
        [SerializeField] private Transform _holdPosition;
        [SerializeField] private Transform _relizePosition;
        [SerializeField] private Transform _skipPosition;

        private List<Product> _serveProducts = new List<Product>();
        private List<Product> _skipProducts = new List<Product>();

        private bool Full => _serveProducts.Count > MaxProductCount[Level];

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Product product))
            {
                if (_serveProducts.Contains(product) || _skipProducts.Contains(product))
                    return;

                if (Full)
                    TryAddProduct(product, RelizeWay.Skip);
                else
                    TryAddProduct(product, RelizeWay.Interact);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Product product))
            {
                if (_serveProducts.Contains(product))
                    _serveProducts.Remove(product);
                else if (_skipProducts.Contains(product))
                    _skipProducts.Remove(product);
            }
        }

        private void TryAddProduct(Product product, RelizeWay way)
        {
            switch (way)
            {
                case RelizeWay.Interact:
                    _serveProducts.Add(product);
                    break;
                case RelizeWay.Skip:
                    _skipProducts.Add(product);
                    break;
            }
            StartCoroutine(ServeProduct(product, way));
        }

        private IEnumerator ServeProduct(Product product, RelizeWay way)
        {
            product.transform.DOMove(_holdPosition.position, _moveDuration);
            product.gameObject.SetActive(false);
            yield return new WaitForSeconds(_moveDuration);

            if (way == RelizeWay.Interact)
            {
                yield return new WaitForSeconds(Modifer[Level]);
                Action(product);
            }

            RelizeProduct(product, way);
        }

        private void RelizeProduct(Product product, RelizeWay way)
        {
            Vector3 position = new Vector3();
            product.gameObject.SetActive(true);

            switch (way)
            {
                case RelizeWay.Interact:
                    position = _relizePosition.position;
                    break;
                case RelizeWay.Skip:
                    position = _skipPosition.position;
                    break;
            }
            product.transform.DOMove(position, _moveDuration);
        }

        protected void AddClonedProduct(Product product)
        {
            _serveProducts.Add(product);
            RelizeProduct(product, RelizeWay.Interact);
        }

        protected abstract void Action(Product product);
    }
}