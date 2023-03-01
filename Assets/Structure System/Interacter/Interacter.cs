using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public abstract class Interacter : Structure
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

        protected abstract void Action(Product product);

        private void OnProductGet(Product product)
        {
            Action(product);
        }
    }
}