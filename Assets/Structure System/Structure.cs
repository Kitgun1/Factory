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
        protected Vector3 Position;

        protected event UnityAction OnUpgrade;

        public bool TryUpgrade()
        {
            if (Level >= MaxLevel) return false;

            Level++;
            OnUpgrade?.Invoke();
            return true;
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