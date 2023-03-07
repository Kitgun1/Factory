using UnityEngine;

namespace Factory
{
    public class Seller : Destroyer
    {
        [SerializeField] private Wallet _wallet;

        private void OnEnable()
        {
            Init();
            OnProductInside += OnProductGet;
        }

        private void OnDisable()
        {
            OnProductInside -= OnProductGet;
        }

        private void OnProductGet(Product product)
        {
            float modifier = SpeedTickModifers[Level];

            if (modifier > 1 || modifier < 0)
                modifier = 1;

            _wallet.CurrencyTransfer(CurrencyType.Coin, (int)(product.Price() * modifier));
        }

        private void OnValidate()
        {
            SpeedTickModifers = LimitList(SpeedTickModifers, Level);
        }
    }
}