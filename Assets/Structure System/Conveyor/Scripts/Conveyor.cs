using UnityEngine;

namespace Factory
{
    public class Conveyor : Structure
    {
        public float CurrentMoveRate() => SpeedTickModifers[Level];

        private void Start()
        {
            Init();
        }

        private void OnValidate()
        {
            LimitList(SpeedTickModifers, MaxLevel);
        }
    }
}