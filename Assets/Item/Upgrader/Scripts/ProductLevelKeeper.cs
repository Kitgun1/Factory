using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
	[CreateAssetMenu(menuName = "Products/ProductLevelKeeper", fileName = "ProductLevelKeeper", order = 1)]
	public class ProductLevelKeeper : ScriptableObject
	{
		[SerializeField] private List<Product> _products = new List<Product>();

		public IReadOnlyCollection<Product> Products => _products;
	}
}