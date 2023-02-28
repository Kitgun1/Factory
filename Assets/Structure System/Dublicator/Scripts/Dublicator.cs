using KiMath;
using UnityEngine;

namespace Factory
{
	public class Dublicator : Interacter
	{
        protected override void Action(Product product)
        {
            if (product.Cloned)
                return;

            Product cloned = Instantiate(product, product.transform.position, Quaternion.identity);
            cloned.Init(product.Templtate, true);
            AddClonedProduct(cloned);
        }

        private void OnValidate()
        {
            LimitModifer(list:Modifer);
            LimitModifer(list:MaxProductCount);
        }
    }
}