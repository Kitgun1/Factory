using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(BoxCollider))]
    public class Destroyer : Structure
    {
        [Layer, SerializeField] private int _layerProduct;
        private IEnumerator _destroyerEnumerator = null;

        private Queue<Product> _inside = new Queue<Product>();

        private void OnValidate()
        {
            LimitList(SpeedTickModifers, MaxLevel);
        }
    }
}