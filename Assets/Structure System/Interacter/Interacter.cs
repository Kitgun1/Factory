using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(Collider))]
    public abstract class Interacter : Structure
    {
        [SerializeField] protected List<float> MaxProductCount;

        private void OnTriggerEnter(Collider other)
        {
            
        }

        protected abstract void Action(Product product);
    }
}