using DG.Tweening;
using KiMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
    public class Structure : MonoBehaviour
    {
        [Min(1), SerializeField] protected int MaxLevel = 5;
        [SerializeField] protected List<float> SpeedTickModifers;
        [SerializeField] protected Transform StayPointProduct;
        [SerializeField] protected List<StructurePointData> Points = new List<StructurePointData>();

        protected int Level;
        protected Quaternion Quaternion;
        protected Vector2Int Position;
        protected MapManage MapManage;
        protected TickManage TickManage;
        protected PriorityType TargetPriority = PriorityType.Main;

        private int _lastOutpoutIndex = 0;
        private IEnumerator _tickEnumerator = null;

        public event UnityAction<Product> ProductInside;
        public event UnityAction<Product> ProductOutside;

        public List<StructurePointData> StructurePoints => Points;
        public StructurePointData? StructurePointWithProduct => this.GetPointWithProduct();
        public void SetPosition(Vector2Int position) => Position = position;
        public void SetQuaternion(Quaternion quaternion) => Quaternion = quaternion;

        protected void Init()
        {
            MapManage = MapManage.Instance;
            TickManage = TickManage.Instance;
            for (int i = 0; i < 4; i++)
            {
                var temp = Points[i];
                temp.Axis = (Direction)i;
                Points[i] = temp;
            }
            StartTickRoutine();
        }

        protected void StartTickRoutine()
        {
            if (_tickEnumerator != null)
            {
                StopTickRoutine();
            }

            _tickEnumerator = TickRoutine();
            StartCoroutine(_tickEnumerator);
        }

        protected void StopTickRoutine()
        {
            if (_tickEnumerator == null) return;

            StopCoroutine(_tickEnumerator);
            _tickEnumerator = null;
        }


        protected IEnumerator TickRoutine()
        {
            yield return new WaitForSeconds(SpeedTickModifers[Level]);

            // Перенести из this Output в Input другой структуры
            StructurePointData? outputPointWithProduct = this.GetPointWithProduct(PointState.Output);
            if (outputPointWithProduct != null)
            {
                Structure structure = MapManage.GetStructure(Position, outputPointWithProduct.Value.Axis);
                if (structure != null)
                {
                    if (structure.StructurePointWithProduct == null)
                    {
                        StructurePointData? inputPoint = structure.GetPoint((outputPointWithProduct.Value.Axis.ToAxis() * -1).ToDirection(), PointState.Input);

                        if (inputPoint != null)
                        {
                            StructurePointData targetInput = (StructurePointData)inputPoint;
                            StructurePointData output = (StructurePointData)outputPointWithProduct;

                            for (int i = 0; i < structure.Points.Count; i++)
                            {
                                if (structure.Points[i].Axis == targetInput.Axis)
                                {
                                    StructurePointData temp = output;
                                    structure.Points[i] = temp;
                                    structure.Points[i].Product.transform.DOMove(structure.StayPointProduct.position, SpeedTickModifers[Level] / 2);
                                    ProductInside?.Invoke(structure.Points[i].Product);
                                    break;
                                }
                            }
                            for (int i = 0; i < Points.Count; i++)
                            {
                                if (Points[i].Axis == output.Axis)
                                {
                                    StructurePointData temp = Points[i];
                                    temp.Product = null;
                                    Points[i] = temp;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // Перенести продукт из this Input в this Output с настройкой
            StructurePointData? inputPointWithProduct = this.GetPointWithProduct(PointState.Input);
            if (inputPointWithProduct != null)
            {
                List<StructurePointData> priorityOutputPoint = this.GetPoints(PointState.Output, TargetPriority);
                List<StructurePointData> withoutPriorityOutputPoint = this.GetPoints(PointState.Output, TargetPriority);

                List<StructurePointData> points = null;
                if (priorityOutputPoint != null) points = priorityOutputPoint;
                else if (withoutPriorityOutputPoint != null) points = withoutPriorityOutputPoint;
                if (points.Count > 0)
                {
                    if (_lastOutpoutIndex > points.Count) _lastOutpoutIndex = 0;
                    StructurePointData targetOutput = points[_lastOutpoutIndex];
                    StructurePointData input = (StructurePointData)inputPointWithProduct;
                    for (int i = 0; i < Points.Count; i++)
                    {
                        if (Points[i].Axis == targetOutput.Axis)
                        {
                            StructurePointData temp = Points[i];
                            temp.Product = targetOutput.Product;
                            Points[i] = temp;
                        }
                    }
                    for (int i = 0; i < Points.Count; i++)
                    {
                        if (Points[i].Axis == input.Axis)
                        {
                            StructurePointData temp = Points[i];
                            temp.Product = null;
                            Points[i] = temp;
                            ProductOutside?.Invoke(Points[i].Product);
                        }
                    }
                    _lastOutpoutIndex++;
                }
            }
            StartTickRoutine();
        }

        protected List<T> LimitList<T>(List<T> list, int max)
        {
            if (list.Count > max)
            {
                list.RemoveRange(max, list.Count - max);
            }
            else if (list.Count < max)
            {
                List<T> tempLevels = new List<T>(list);
                for (int i = 0; i < max - list.Count; i++)
                {
                    tempLevels.Add(default(T));
                }
            }
            return list;
        }
    }
}