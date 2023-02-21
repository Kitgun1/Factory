namespace Factory
{
    public class Conveyor : Structure
    {
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
    }
}