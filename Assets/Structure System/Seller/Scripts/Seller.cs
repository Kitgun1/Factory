using System.Collections;
using UnityEngine;

namespace Factory
{
	public class Seller : Destroyer
	{
        [SerializeField] private Wallet _wallet;

        protected override IEnumerator DestroyerRoutine()
        {
            _wallet.CurrencyTransfer(CurrencyType.Coin, Modifer[Level]);
            return base.DestroyerRoutine();
        }
    }
}