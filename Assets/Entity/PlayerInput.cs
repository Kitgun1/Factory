using UnityEngine;

namespace Shoping
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private EntityMovementData _movementData;
        [SerializeField] private PlayerAnimation _animation;

        private EntityMovement _entityMovement;

        private void Awake()
        {
            _entityMovement = new EntityMovement(_movementData);
        }

        private void Update()
        {
            Vector2 direction = GetDirection(PlatformType.Desctop);

            _entityMovement.Move(direction);
            _entityMovement.Rotate(_movementData.Transform, direction);
            _animation.SetAnimationFloat(AnimationParamType.Speed, GetSpeed(direction));
        }

        public Vector2 GetDirection(PlatformType type)
        {
            switch (type)
            {
                case PlatformType.Desctop:
                    return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                case PlatformType.Mobile:
                    return _joystick.Direction;
                default:
                    return Vector2.zero;
            }

        }

        private float GetSpeed(Vector2 vector)
        {
            if (vector.x == 0 && vector.y == 0) return 0f;

            float speed = 0;
            if (Mathf.Abs(vector.x) > 0f) speed = Mathf.Abs(vector.x); 
            if (Mathf.Abs(vector.y) > speed) speed = Mathf.Abs(vector.y);

            return speed;
        }
    }
}