using KiMath;
using UnityEngine;

namespace Factory
{
	public class Dublicator : Interacter
	{
        protected override void Action(Product product)
        {
            Product cloned = Instantiate(product, product.transform.position, Quaternion.identity);

        }

        private void OnValidate()
        {
            LimitModifer(list:Modifer);
            LimitModifer(list:MaxProductCount);
        }
    }
}