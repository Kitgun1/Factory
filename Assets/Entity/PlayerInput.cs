using UnityEngine;

namespace Factory
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private EntityMovementData _movementData;
        [SerializeField] private PlayerAnimation _animation;

        private EntityMovement _entityMovement;
        private Vector2 _direction = Vector2.zero;

       [SerializeField] private MapManage _mapManage;

        private void Awake()
        {
            _entityMovement = new EntityMovement(_movementData);
        }

        private void Update()
        {
            _direction = GetDirection(PlatformType.Desctop);

            _animation.SetAnimationFloat(AnimationParamType.Speed, GetSpeed(_direction));

            _mapManage.GetNearCell(transform.position);
        }

        private void FixedUpdate()
        {
            _entityMovement.Move(_direction);
            _entityMovement.Rotate(_movementData.Transform, _direction);
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