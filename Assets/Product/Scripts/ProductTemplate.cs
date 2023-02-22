using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public abstract class ProductTemplate : ScriptableObject
	{
        public abstract int MaxLevel();
    }
}