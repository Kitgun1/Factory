using UnityEngine;

namespace Factory
{
    public class Conveyor : Structure
    {
        public float CurrentMoveRate() => Modifer[Level];

        private void Start()
        {
            Init();
        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
        }
    }
}