using UnityEngine;

namespace Shoping
{
    public class EntityMovement : IMove, IRotate
    {
        private EntityMovementData _data;
        private Vector3 _velocity = Vector3.zero;
        private const float _multiplyMove = 100;
        private const float _multiplyRotate = 360;

        public EntityMovement(EntityMovementData data) => _data = data;

        public void Move(Vector2 direction)
        {
            Vector3 targetVelocity = new Vector3(direction.x, 0f, direction.y) * _data.MovementSpeed * _multiplyMove;
            _data.Rigidbody.velocity = Vector3.SmoothDamp(_data.Rigidbody.velocity, targetVelocity, ref _velocity, _data.MovementSmooth) * Time.deltaTime;
        }

        public void Rotate(Transform transform, Vector2 direction)
        {
            if (direction == Vector2.zero) return;

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.y));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _data.RotationSpeed * _multiplyRotate * Time.deltaTime);
            _data.Rigidbody.angularVelocity = Vector3.zero;
        }
    }
}