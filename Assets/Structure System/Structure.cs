using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class Structure : MonoBehaviour
    {
        [Min(1), SerializeField] protected int MaxLevel = 10;
        [SerializeField] protected List<float> Modifer;

        protected int Level;
        protected Quaternion Quaternion;
        protected Vector2Int Position;

        protected StructurePoint PointUp;
        protected StructurePoint PointRight;
        protected StructurePoint PointDown;
        protected StructurePoint PointLeft;

        //protected event UnityAction OnUpgrade;

        private MapManage _mapManage;
        private IEnumerator _sendEnumerator = null;

        public void SetPosition(Vector2Int position) => Position = position;
        public void SetQuaternion(Quaternion quaternion) => Quaternion = quaternion;

        protected virtual void Init()
        {
            _mapManage = MapManage.Instance;
            PointUp = new StructurePoint(new Vector2Int(0, 1));
            PointRight = new StructurePoint(new Vector2Int(1, 0));
            PointDown = new StructurePoint(new Vector2Int(0, -1));
            PointLeft = new StructurePoint(new Vector2Int(-1, 0));
        }

        public virtual void StartRoutine()
        {
            if (_sendEnumerator != null) StopRoutine();

            _sendEnumerator = Routine();
            StartCoroutine(_sendEnumerator);
        }

        public virtual void StopRoutine()
        {
            if (_sendEnumerator == null) return;

            StopCoroutine(_sendEnumerator);
            _sendEnumerator = null;
        }

        private IEnumerator Routine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Modifer[Level]);
                List<StructurePoint> pointsOutput = GetPoints(StructurePointState.Output);
                List<StructurePoint> pointsInput = GetPoints(StructurePointState.Input);

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
                                pointInput.Product = null;
                            }
                        }
                    }
                }

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
            if (axis == new Vector2Int(0, 1)) return PointUp;
            else if (axis == new Vector2Int(1, 0)) return PointRight;
            else if (axis == new Vector2Int(0, -1)) return PointDown;
            else if (axis == new Vector2Int(-1, 0)) return PointLeft;

            return null;
        }

        public List<StructurePoint> GetPoints(StructurePointState state)
        {
            List<StructurePoint> result = new List<StructurePoint>();

            if (PointUp.State == state)
                result.Add(PointUp);
            if (PointRight.State == state)
                result.Add(PointRight);
            if (PointDown.State == state)
                result.Add(PointDown);
            if (PointLeft.State == state)
                result.Add(PointLeft);

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