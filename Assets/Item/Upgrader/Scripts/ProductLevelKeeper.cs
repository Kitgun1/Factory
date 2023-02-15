using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
	public class ProductLevelKeeper : MonoBehaviour
	{
        [SerializeField] private List<ProductLevelInfo> _products = new List<ProductLevelInfo>();

        [SerializeField] public int MaxLevel => _products.Count - 1;
        public ProductLevelInfo GetProductInfo(int level) => _products[level];
		public static ProductLevelKeeper Instance;

        private void Awake()
        {
            Instance = this;
        }
    }

    [System.Serializable]
    public struct ProductLevelInfo
    {
        public Material Material;
        public Mesh Mesh;
    }
}