using UnityEngine;

namespace Factory
{
    public abstract class ProductTemplate : ScriptableObject
	{
        public Product Product;
        public abstract long Price(int level);
        public abstract int MaxLevel();
    }
}