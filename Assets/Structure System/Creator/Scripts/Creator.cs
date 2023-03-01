using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
    public class Creator : Structure, ITurn
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private List<Material> _materials;
        [SerializeField] private ProductTemplate _template;
        [SerializeField] private float _rotationDuration;

        private IEnumerator _createEnumerator = null;

        public float CurrentMoveRate => Modifer[Level];
        public ProductTemplate CurrentTemplate => _template;

        private void Awake()
        {
            Init();
            StartRoutine();
        }

        public override void StartRoutine()
        {
            if (_createEnumerator != null)
                StopRoutine();

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
                List<StructurePoint> pointsOutput = GetPointsByState(StructurePointState.Output);

                if (pointsOutput.Count > 0)
                {
                    foreach (var pointOutput in pointsOutput)
                    {
                        if (pointOutput.Product == null)
                        {
                            var product = Instantiate(CurrentTemplate.GameObject, StayPointProduct.position, Quaternion.identity, transform);
                            pointOutput.Product = product;
                            break;
                        }
                    }
                }
            }
        }













        private void OnUpgraded()
        {
            SetUpgrade(Level);
        }

        private void OnProductSpawned(Product product)
        {
            product.Init(_template, false);
        }

        private void SetUpgrade(int level = 0)
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

        public void Rotate(Transform transform)
        {
            EntityTurner.Action(transform, _rotationDuration);
        }
    }
}