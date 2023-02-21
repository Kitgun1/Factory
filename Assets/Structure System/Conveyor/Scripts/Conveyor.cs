using UnityEngine;
using System.Collections.Generic;

namespace Factory
{
    public class Conveyor : Item, IRotate
    {
        [SerializeField] private float _rotationDuration;

        private void OnEnable()
        {
            OnUpgrade += OnUpgraded;
        }

        private void OnDisable()
        {
            OnUpgrade -= OnUpgraded;
        }

        private void OnUpgraded()
        {

        }

        public void Rotate(Transform transform, Vector2 direction)
        {
            EntityTurner.Action(transform, _rotationDuration);
        }
    }
}