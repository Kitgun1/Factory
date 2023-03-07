namespace Factory
{
    public class VisualUpgrader : Interacter
    {
        private void OnEnable()
        {
            Init();
            ProductInside += OnProductGet;
        }

        private void OnDisable()
        {
            ProductInside -= OnProductGet;
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