using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
    public class Item : MonoBehaviour
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

        public void LimitModifer()
        {
            if (Modifer.Count > MaxLevel)
            {
                Modifer.RemoveRange(MaxLevel, Modifer.Count - MaxLevel);
            }
            else if (Modifer.Count < MaxLevel)
            {
                List<float> tempModifer = new List<float>(Modifer);
                for (int i = 0; i < MaxLevel - Modifer.Count; i++)
                {
                    tempModifer.Add(0);
                }
                Modifer = new List<float>(tempModifer);
            }
        }
    }
}