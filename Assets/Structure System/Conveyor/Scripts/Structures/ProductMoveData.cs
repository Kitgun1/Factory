using NaughtyAttributes;
using System;
using UnityEngine;

namespace Factory
{
    [Serializable]
    public struct ProductMoveData
    {
        public Product Product;
        public Conveyor Conveyor;

        public ProductMoveData(Product product, Conveyor conveyor)
        {
            Product = product;
            Conveyor = conveyor;
        }
    }
}