using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(BoxCollider))]
    public class Seller : Item
    {
        [Layer, SerializeField] private int _layerProduct;
        private IEnumerator _sellerEnumerator = null;

        private Queue<GameObject> _inside = new Queue<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != _layerProduct) return;

            _inside.Enqueue(other.gameObject);
            StartSellerRoutine();
        }

        public void StartSellerRoutine()
        {
            if (_sellerEnumerator != null) return;

            _sellerEnumerator = SellerRoutine();
            StartCoroutine(_sellerEnumerator);
        }

        public void StopSellerRoutine()
        {
            if (_sellerEnumerator == null) return;
            StopCoroutine(_sellerEnumerator);
            _sellerEnumerator = null;
        }

        private IEnumerator SellerRoutine()
        {
            while (true)
            {
                if (Modifer[Level] <= 0) Modifer[Level] = 0.2f;
                yield return new WaitForSeconds(Modifer[Level]);

                GameObject nextProduct = _inside.Peek();
                _inside.Dequeue();
                // Sell item
                Destroy(nextProduct);

                if(_inside.Count == 0)
                {
                    StopSellerRoutine();
                }
            }
        }
    }
}