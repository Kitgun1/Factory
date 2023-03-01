using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(BoxCollider))]
    public class Destroyer : Structure
    {
        private void OnEnable()
        {
            Init();
            ProductGet += OnProductGet;
        }

        private void OnDisable()
        {
            ProductGet -= OnProductGet;
        }

        private void OnProductGet(Product product)
        {

        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
        }
    }
}