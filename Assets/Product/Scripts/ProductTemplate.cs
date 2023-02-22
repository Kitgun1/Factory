using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public abstract class ProductTemplate : ScriptableObject
	{
        public Product GameObject;
        public abstract int MaxLevel();
    }
}