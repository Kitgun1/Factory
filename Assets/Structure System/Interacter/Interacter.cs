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
            product.transform.DOMove(_holdPosition.position, _moveDuration);

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
            yield return new WaitForSeconds(_moveDuration);
            product.gameObject.SetActive(false);

            switch (way)
            {
                case RelizeWay.Interact:
                    yield return new WaitForSeconds(Modifer[Level]);
                    Action(product);
                    RelizeProduct(product, _relizePosition);
                    break;
                case RelizeWay.Skip:
                    RelizeProduct(product, _skipPosition);
                    break;
            }

        }

        private void RelizeProduct(Product product, Transform transform)
        {
            product.gameObject.SetActive(true);
            product.transform.DOMove(transform.position, _moveDuration);
        }

        protected abstract void Action(Product product);
    }
}