using System.Collections;
using System.Numerics;
using UnityEngine;

namespace Factory
{
	public class Seller : Destroyer
	{
        [SerializeField] private Wallet _wallet;

        private void OnEnable()
        {
            Init();
            ProductGet += OnProductGet;
        }

        private void OnDisable()
        {
            ProductGet -= OnProductGet;
        }

        private void OnProductGet(Product product)
        {
            float modifier = Modifer[Level];

            if (modifier > 1 || modifier < 0)
                modifier = 1;

            _wallet.CurrencyTransfer(CurrencyType.Coin, (int)(product.Price() * modifier));
        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
        }
    }
}