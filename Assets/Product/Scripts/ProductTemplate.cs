using UnityEngine;

namespace Factory
{
    public abstract class ProductTemplate : ScriptableObject
	{
        public Product Product;
        public ProductType Type;
        public abstract double Price(int level);
        public abstract int MaxLevel();
    }
}