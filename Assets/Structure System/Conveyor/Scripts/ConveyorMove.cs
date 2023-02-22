using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class ConveyorMove : MonoBehaviour
    {
        [Layer, SerializeField] private int _sortingLayer;
        [SerializeField] private float _speed;
        [MinValue(-1), MaxValue(1), SerializeField] private Vector2Int _direction;

        private List<Transform> _products = new List<Transform>();

        private void OnTriggerEnter(Collider other)
        {
            var gameObject = other.gameObject;
            if (_sortingLayer != gameObject.layer) return;
            _products.Add(gameObject.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            var gameObject = other.gameObject;
            if (_sortingLayer != gameObject.layer) return;
            _products.Remove(gameObject.transform);
        }

        private void Update()
        {
            foreach (var product in _products)
            {
                product.transform.Translate(new Vector3(_direction.x, 0, _direction.y) * _speed * Time.deltaTime);
            }
        }
    }
}