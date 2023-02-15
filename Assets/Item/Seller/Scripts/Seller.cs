using System.Collections;
using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(BoxCollider))]
    public class Seller : MonoBehaviour
    {
        private IEnumerator _sellerEnumerator = null;

        private void OnTriggerEnter(Collider other)
        {
        }

        private IEnumerator SellerRoutine()
        {
            yield return null;
        }
    }
}