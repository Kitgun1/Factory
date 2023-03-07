namespace Factory
{
    public class VisualUpgrader : Interacter
    {
        private void OnEnable()
        {
            Init();
            OnProductInside += OnProductGet;
        }

        private void OnDisable()
        {
            OnProductInside -= OnProductGet;
        }

        private void OnValidate()
        {
            SpeedTickModifers = LimitList(SpeedTickModifers, Level);
        }

        protected override void Action(Product product)
        {
            if(product is IUpgradeable upgradeable)
                upgradeable.TryUpgrade();
        }
    }
}