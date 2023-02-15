using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
    public class Item : MonoBehaviour
    {
        [Min(1), SerializeField] protected int MaxLevel = 10;
        protected int Level;
        protected Quaternion Quaternion;
        protected Vector3 Position;

        protected event UnityAction OnUpgrade;

        protected bool TryUpgrade()
        {
            if (Level >= MaxLevel) return false;

            Level++;
            OnUpgrade?.Invoke();
            return true;
        }
    }
}