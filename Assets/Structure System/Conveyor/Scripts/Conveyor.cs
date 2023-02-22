using UnityEngine;

namespace Factory
{
    public class Conveyor : Structure
    {
        [SerializeField] private Vector2 Direction = Vector2.down;

        public Vector2 CurrentDirection() => Direction;
        public float CurrentSpeed() => Modifer[Level];

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
            Level++;
        }

        private void OnValidate()
        {
            LimitModifer();
        }
    }
}