using NaughtyAttributes;
using UnityEngine;

namespace Factory
{
    [RequireComponent(typeof(Conveyor))]
    [RequireComponent(typeof(BoxCollider))]
    public class ConveyorMove : MonoBehaviour
    {
        [Layer, SerializeField] private int _sortingLayer;

        private MoverManage _mover;
        private Conveyor _conveyor;

        private void Start()
        {
            _conveyor = GetComponent<Conveyor>();
            _mover = MoverManage.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            var gameObject = other.gameObject;
            if (_sortingLayer != gameObject.layer) return;
            var product = gameObject.GetComponent<Product>();
            _mover.AddProductMove(product, _conveyor);
        }

        private void OnTriggerExit(Collider other)
        {
            var gameObject = other.gameObject;
            if (_sortingLayer != gameObject.layer) return;
            var product = gameObject.GetComponent<Product>();
            _mover.TryRemoveProductMove(product);
        }
    }
}