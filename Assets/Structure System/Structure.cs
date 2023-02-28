using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        protected event UnityAction OnUpgrade;

        private IEnumerator _sendEnumerator = null;

        public void SetPosition(Vector2Int position) => Position = position;
        public void SetQuaternion(Quaternion quaternion) => Quaternion = quaternion;

        protected virtual void Init()
        {
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
                
            }
        }

        public virtual bool TryUpgrade()
        {
            if (Level >= MaxLevel) return false;

            Level++;
            OnUpgrade?.Invoke();
            return true;
        }

        public StructurePoint GetStructurePoint(StructurePointState state, Vector2Int axis)
        {
            var points = GetStructurePoints(state);
            foreach (var point in points)
                if (point.Axis == axis) return point;

            return null;
        }

        public StructurePoint GetStructurePoint(Vector2Int axis)
        {
            if (axis == new Vector2Int(0, 1)) return PointUp;
            else if (axis == new Vector2Int(1, 0)) return PointRight;
            else if (axis == new Vector2Int(0, -1)) return PointDown;
            else if (axis == new Vector2Int(-1, 0)) return PointLeft;

            return null;
        }

        public List<StructurePoint> GetStructurePoints(StructurePointState state)
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

        public virtual void LimitModifer()
        {
            if (Modifer.Count > MaxLevel)
            {
                Modifer.RemoveRange(MaxLevel, Modifer.Count - MaxLevel);
            }
            else if (Modifer.Count < MaxLevel)
            {
                List<float> tempLevels = new List<float>(Modifer);
                for (int i = 0; i < MaxLevel - Modifer.Count; i++)
                {
                    tempLevels.Add(0f);
                }
                Modifer = new List<float>(tempLevels);
            }
        }
    }
}