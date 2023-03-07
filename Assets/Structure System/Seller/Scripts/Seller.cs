using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class Seller : Destroyer
    {
        [field: SerializeField] private List<float> SellPriceModifier;
        [SerializeField] private Wallet _wallet;

        private void OnEnable()
        {
            Init();
            ProductInside += OnProductGet;
        }

        private void OnDisable()
        {
            ProductInside -= OnProductGet;
        }

        protected override void OnProductGet(Product product)
        {
            float modifier = SellPriceModifier[Level];

            if (modifier > 1 || modifier < 0)
                modifier = 0;
            modifier += 1;

            _wallet.CurrencyTransfer(CurrencyType.Coin, (int)(product.Price() * modifier));
        }

        private void OnValidate()
        {
            SpeedTickModifers = LimitList(SpeedTickModifers, Level);
            SellPriceModifier = LimitList(SellPriceModifier, Level);
        }
    }
}