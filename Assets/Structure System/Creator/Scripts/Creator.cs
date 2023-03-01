using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{

    public class Creator : Structure
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private List<Material> _materials;
        [SerializeField] private ProductTemplate _template;

        private IEnumerator _createEnumerator = null;

        public float CurrentMoveRate => Modifer[Level];
        public ProductTemplate CurrentTemplate => _template;

        private void Start()
        {
            Init();
            SetUpgrade();
        }

        public override void StartRoutine()
        {
            if (_createEnumerator != null) StopRoutine();

            _createEnumerator = CreateRoutine();
            StartCoroutine(_createEnumerator);

            base.StartRoutine();
        }

        public override void StopRoutine()
        {
            if (_createEnumerator != null)
            {
                StopCoroutine(_createEnumerator);
                _createEnumerator = null;
            }
            base.StopRoutine();
        }

        private IEnumerator CreateRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(CurrentMoveRate);
                var pointsOutput = GetPointsByState(StructurePointState.Output);
                foreach (var point in pointsOutput)
                {
                    if (point.Product == null)
                    {
                        var product = Instantiate(_template.Product, StayPointProduct.position, Quaternion.identity, transform);
                        point.Product = product;
                        OnProductSpawned(product);
                        break;
                    }
                }
            }
        }

        private void OnProductSpawned(Product product)
        {
            product.Init(_template, false);
        }

        private void SetUpgrade(int level = -1)
        {
            if (level < 0)
                level = 0;

            _renderer.material = _materials[level];
        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
            LimitModifer(_materials);
        }
    }
}