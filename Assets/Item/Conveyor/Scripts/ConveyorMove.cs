using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
	public class ConveyorMove : MonoBehaviour
	{
        [Layer, SerializeField] private int _sortingLayer;
        [SerializeField] private float _speed;

        private List<Rigidbody> _bodies = new List<Rigidbody>();

        private void OnTriggerEnter(Collider other)
        {
            var gameObject = other.gameObject;
            if (gameObject.layer != _sortingLayer) return;
            _bodies.Add(gameObject.GetComponent<Rigidbody>());
        }

        private void OnTriggerExit(Collider other)
        {
            var gameObject = other.gameObject;
            if (gameObject.layer != _sortingLayer) return;
            _bodies.Remove(gameObject.GetComponent<Rigidbody>());
        }

        private void FixedUpdate()
        {
            foreach (var body in _bodies)
            {
                body.AddForce(transform.forward * -1f * _speed);
            }
        }
    }
}