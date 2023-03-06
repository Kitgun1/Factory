using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Factory
{

    public class Creator : Structure
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private List<Material> _materials;
        [SerializeField] private ProductTemplate _template;

        private IEnumerator _createEnumerator = null;

        public float CurrentMoveRate => SpeedTickModifers[Level];
        public ProductTemplate CurrentTemplate => _template;

        private void Start()
        {
            Init();
            SetUpgrade();
        }

        public void StartRoutine()
        {
            if (_createEnumerator != null) StopRoutine();

            _createEnumerator = CreateRoutine();
            StartCoroutine(_createEnumerator);
        }

        public void StopRoutine()
        {
            if (_createEnumerator != null)
            {
                StopCoroutine(_createEnumerator);
                _createEnumerator = null;
            }
        }

        private IEnumerator CreateRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(CurrentMoveRate);
                List<StructurePointData> pointsOutput = this.GetPoints(PointState.Output);

                List<StructurePointData> points = this.GetPoints(PointState.Output);

                CreateProduct(points);
            }
        }

        private void CreateProduct(List<StructurePointData> points)
        {
            List<StructurePointData> pointsPriority = this.GetPoints(PointState.Output, PriorityType.Main);
            List<StructurePointData> pointsWithoutPriority = this.GetPoints(PointState.Output, PriorityType.Secendory);
            List<StructurePointData> resultPoints;

            if (pointsPriority.Count > 0)
                resultPoints = pointsPriority;
            else if (pointsWithoutPriority.Count > 0)
                resultPoints = pointsWithoutPriority;
            else return;

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < resultPoints.Count; j++)
                {
                    if (resultPoints[i].Axis == points[i].Axis * -1 && resultPoints[i].Product == null && points[i].Product == null)
                    {
                        var product = Instantiate(_template.Product, StayPointProduct.position, Quaternion.identity, transform);
                        var temp = points[i];
                        temp.Product = product;
                        points[i] = temp;
                        OnProductSpawned(product);
                        return;
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
            LimitList(SpeedTickModifers, MaxLevel);
            LimitList(_materials, MaxLevel);
        }
    }
}