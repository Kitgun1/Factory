using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(BoxCollider))]
    public class Destroyer : Structure
    {
        [Layer, SerializeField] private int _layerProduct;
        private IEnumerator _destroyerEnumerator = null;

        private Queue<GameObject> _inside = new Queue<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != _layerProduct) return;

            _inside.Enqueue(other.gameObject);
            StartDestroyerRoutine();
        }

        public void StartDestroyerRoutine()
        {
            if (_destroyerEnumerator != null) return;

            _destroyerEnumerator = DestroyerRoutine();
            StartCoroutine(_destroyerEnumerator);
        }

        public void StopDestroyerRoutine()
        {
            if (_destroyerEnumerator == null) return;
            StopCoroutine(_destroyerEnumerator);
            _destroyerEnumerator = null;
        }

        protected virtual IEnumerator DestroyerRoutine()
        {
            while (true)
            {
                if (Modifer[Level] <= 0) Modifer[Level] = 0.2f;
                yield return new WaitForSeconds(Modifer[Level]);

                GameObject nextProduct = _inside.Peek();
                _inside.Dequeue();
                Destroy(nextProduct);

                if(_inside.Count == 0)
                {
                    StopDestroyerRoutine();
                }
            }
        }

        private void OnValidate()
        {
            LimitModifer();
        }
    }
}