using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
	public class Seller : Destroyer
	{
        [SerializeField] private Wallet _wallet;
        [SerializeField] private List<float> SellQuality;

        private void OnEnable()
        {
            Init();
            _wallet = FindObjectOfType<Wallet>();
            ProductGet += OnProductGet;
        }

        private void OnDisable()
        {
            ProductGet -= OnProductGet;
        }

        protected override void OnProductGet(Product product)
        {
            double modifier = SellQuality[Level] * 0.01f;

            if (modifier > 1 || modifier < 0)
                modifier = 1;
            
            _wallet.CurrencyTransfer(CurrencyType.Coin, Mathf.RoundToInt((float)(product.Price() * modifier)));
            base.OnProductGet(product);
        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
        }
    }
}