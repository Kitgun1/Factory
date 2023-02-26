using UnityEngine;

namespace Factory
{
    public class Conveyor : Structure
    {
        [SerializeField] private Vector2 _direction = Vector2.down;
        [SerializeField] private Transform _endPoint;

        public Vector2 CurrentDirection() => _direction;
        public Vector3 CurrentEndPoint() => _endPoint.position;
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