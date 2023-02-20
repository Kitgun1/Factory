using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public abstract class Product : ScriptableObject
	{
        public abstract int MaxLevel();
    }
}