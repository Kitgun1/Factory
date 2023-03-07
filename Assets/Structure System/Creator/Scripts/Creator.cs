using KiMath;
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

        public float CreateSpeed => SpeedTickModifers[Level];
        public ProductTemplate CurrentTemplate => _template;

        private void Start()
        {
            Init();
            SetUpgrade();
            StartRoutine();
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
            yield return new WaitForSeconds(CreateSpeed);

            CreateProduct();
            StartRoutine();
        }

        private void CreateProduct()
        {
            List<StructurePointData> pointsPriority = this.GetPoints(PointState.Output, PriorityType.Main);
            List<StructurePointData> pointsWithoutPriority = this.GetPoints(PointState.Output, PriorityType.Secendory);
            List<StructurePointData> points;

            StructurePointData? pointWithProduct = this.GetPointWithProduct();
            if (pointWithProduct != null) return;

            if (pointsPriority.Count > 0)
                points = pointsPriority;
            else if (pointsWithoutPriority.Count > 0)
                points = pointsWithoutPriority;
            else return;

            List<Structure> structures = MapManage.GetStructures(Position);
            List<StructurePointData> anotherPoints = new List<StructurePointData>();
            for (int i = 0; i < structures.Count; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    StructurePointData? anotherPoint = structures[i].GetPoint((points[j].Axis.ToAxis() * -1).ToDirection(), PointState.Input);
                    if (anotherPoint != null)
                    {
                        //if (point.Value.Product != null) // if another input point empty =>
                        anotherPoints.Add((StructurePointData)anotherPoint);
                    }
                }
            }
            if (anotherPoints.Count == 0) return;

            List<StructurePointData> relatedOutputs = new List<StructurePointData>();
            foreach (var anotherPoint in anotherPoints)
            {
                foreach (var point in points)
                {
                    if (point.Axis.ToAxis() == anotherPoint.Axis.ToAxis() * -1)
                    {
                        relatedOutputs.Add(point);
                    }
                }
            }

            for (int i = 0; i < relatedOutputs.Count; i++)
            {
                Product product = Instantiate(_template.Product, StayPointProduct.position, Quaternion.identity, transform);
                for (int j = 0; j < Points.Count; j++)
                {
                    if (Points[j].Axis == relatedOutputs[i].Axis)
                    {
                        StructurePointData temp = Points[j];
                        temp.Product = product;
                        Points[j] = temp;
                    }
                }
                OnProductSpawned(product);
                return;
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
            SpeedTickModifers = LimitList(SpeedTickModifers, MaxLevel);
            _materials = LimitList(_materials, MaxLevel);
        }
    }
}