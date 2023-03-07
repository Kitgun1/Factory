namespace Factory
{
    public class Conveyor : Structure
    {
        private void Start()
        {
            Init();
        }

        private void OnValidate()
        {
            SpeedTickModifers = LimitList(SpeedTickModifers, MaxLevel);
        }
    }
}