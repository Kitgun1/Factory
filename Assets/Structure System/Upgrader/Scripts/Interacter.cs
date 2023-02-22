using KiMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Factory
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interacter : Structure
    {
        [SerializeField] protected List<float> MaxProductCount;
        [SerializeField] private Transform _holdPosition;
        [SerializeField] private Transform _relesePosition;

        private List<Product> _productsInside = new List<Product>();
        private static float _fieldWidth = 1;

        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Product product))
            {
                Action(product);
                product.transform.position = _holdPosition.position;
                _productsInside.Add(product);
                OnProductAdded();
            }
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

        private IEnumerator ReleaseProducts(float delay)
        {
            yield return new WaitForSeconds(delay);
            float[] positions = MathK.SplitWay(_fieldWidth, _productsInside[0].transform.localScale.x, _productsInside.Count);

            for (int i = 0; i < _productsInside.Count; i++)
            {
                _productsInside[i].transform.position = new Vector3(positions[i], _relesePosition.position.y, _relesePosition.position.z);
            }

            _productsInside.Clear();
        }

        protected abstract void Action(Product product);

        private void OnProductAdded()
        {
            if (_productsInside.Count >= MaxProductCount[Level])
                StartCoroutine(ReleaseProducts(Modifer[Level]));
        }
    }
}