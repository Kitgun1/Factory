using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
    public abstract class Structure : MonoBehaviour
    {
        [Min(1), SerializeField] protected int MaxLevel = 10;
        [SerializeField] protected List<float> Modifer;
        [SerializeField] protected Transform PointProduct;
        [SerializeField] protected StrucurePointsAxisData StructurePointsAxis;


        protected int Level;
        protected Quaternion Quaternion;
        protected Vector2Int Position;

        //protected event UnityAction OnUpgrade;
        protected event UnityAction<Product> OnProductGet;

        private MapManage _mapManage;
        private IEnumerator _sendEnumerator = null;

        public void SetPosition(Vector2Int position) => Position = position;
        public void SetQuaternion(Quaternion quaternion) => Quaternion = quaternion;

        protected virtual void Init()
        {
            _mapManage = MapManage.Instance;

            StructurePointsAxis.PointUp = new StructurePoint(StructurePointsAxis.Up, new Vector2Int(0, 1));
            StructurePointsAxis.PointRight = new StructurePoint(StructurePointsAxis.Right, new Vector2Int(1, 0));
            StructurePointsAxis.PointDown = new StructurePoint(StructurePointsAxis.Down, new Vector2Int(0, -1));
            StructurePointsAxis.PointLeft = new StructurePoint(StructurePointsAxis.Left, new Vector2Int(-1, 0));
        }

        public virtual void StartRoutine()
        {
            if (_sendEnumerator != null) StopRoutine();

            _sendEnumerator = SendRoutine();
            StartCoroutine(_sendEnumerator);
        }

        public virtual void StopRoutine()
        {
            if (_sendEnumerator != null)
            {
                StopCoroutine(_sendEnumerator);
                _sendEnumerator = null;
            }
        }

        private IEnumerator SendRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Modifer[Level]);
                List<StructurePoint> pointsOutput = GetPoints(StructurePointState.Output);
                List<StructurePoint> pointsInput = GetPoints(StructurePointState.Input);

                // Point move to output
                if (pointsInput.Count != 0 || pointsOutput.Count != 0)
                {
                    foreach (var pointInput in pointsInput)
                    {
                        if (pointInput.Product != null)
                        {
                            StructurePoint emptyPointOutput = null;
                            foreach (var pointOutput in pointsOutput)
                            {
                                if (pointOutput.Product == null)
                                {
                                    emptyPointOutput = pointOutput;
                                    break;
                                }
                            }

                            if (emptyPointOutput != null)
                            {
                                emptyPointOutput.Product = pointInput.Product;
                                OnProductGet?.Invoke(emptyPointOutput.Product);
                                pointInput.Product = null;
                            }
                        }
                    }
                }

                // Point get from another object
                foreach (var pointInput in pointsInput)
                {
                    Vector2Int position = new Vector2Int(Position.x + pointInput.Axis.x, Position.y + pointInput.Axis.x);
                    var structure = _mapManage.GetStructure(position.x, position.y);
                    if (structure != null)
                    {
                        var pointOutput = structure.GetPointByState(StructurePointState.Output, new Vector2Int(pointInput.Axis.x * -1, pointInput.Axis.y * -1));
                        if (pointOutput.Product != null)
                        {
                            pointInput.Product = pointOutput.Product;
                            pointOutput.Product = null;
                        }
                    }
                }
            }
        }

        public StructurePoint GetPointByState(StructurePointState state, Vector2Int axis)
        {
            var points = GetPoints(state);
            foreach (var point in points)
                if (point.Axis == axis) return point;

            return null;
        }

        public StructurePoint GetPoint(Vector2Int axis)
        {
            if (axis == new Vector2Int(0, 1)) return StructurePointsAxis.PointUp;
            else if (axis == new Vector2Int(1, 0)) return StructurePointsAxis.PointRight;
            else if (axis == new Vector2Int(0, -1)) return StructurePointsAxis.PointDown;
            else if (axis == new Vector2Int(-1, 0)) return StructurePointsAxis.PointLeft;

            return null;
        }

        public List<StructurePoint> GetPoints(StructurePointState state)
        {
            List<StructurePoint> result = new List<StructurePoint>();

            if (StructurePointsAxis.PointUp.State == state)
                result.Add(StructurePointsAxis.PointUp);
            if (StructurePointsAxis.PointRight.State == state)
                result.Add(StructurePointsAxis.PointRight);
            if (StructurePointsAxis.PointDown.State == state)
                result.Add(StructurePointsAxis.PointDown);
            if (StructurePointsAxis.PointLeft.State == state)
                result.Add(StructurePointsAxis.PointLeft);

            if (result.Count == 0)
                return null;

            return result;
        }

        protected void LimitModifer(List<float> list)
        {
            if (list.Count > MaxLevel)
            {
                list.RemoveRange(MaxLevel, list.Count - MaxLevel);
            }
            else if (list.Count < MaxLevel)
            {
                List<float> tempLevels = new List<float>(list);
                for (int i = 0; i < MaxLevel - list.Count; i++)
                {
                    tempLevels.Add(0f);
                }
                list = new List<float>(tempLevels);
            }
        }

        protected void LimitModifer(List<Material> list)
        {
            if (list.Count > MaxLevel)
            {
                list.RemoveRange(MaxLevel, list.Count - MaxLevel);
            }
            else if (list.Count < MaxLevel)
            {
                List<Material> tempLevels = new List<Material>(list);
                for (int i = 0; i < MaxLevel - list.Count; i++)
                {
                    tempLevels.Add(null);
                }
                list = new List<Material>(tempLevels);
            }
        }
    }
}