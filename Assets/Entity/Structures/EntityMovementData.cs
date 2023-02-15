using System;
using UnityEngine;

namespace Factory
{
    [Serializable]
    public struct EntityMovementData
    {
        public Rigidbody Rigidbody;
        public Transform Transform;

        public float RotationSpeed;
        public float MovementSpeed;
        [Range(0f, 0.1f)] public float MovementSmooth;
    }
}