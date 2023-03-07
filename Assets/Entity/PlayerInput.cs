using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private List<PlayerHotKeyData> _hotKeys;
        [SerializeField] private EntityMovementData _movementData;
        [SerializeField] private PlayerAnimation _animation;

        private EntityMovement _entityMovement;
        private Vector2 _direction = Vector2.zero;
        private Input _input;

        public event UnityAction<PlayerActionType> OnHotKeyPress;

        [SerializeField] private MapManage _mapManage;

        private void Awake()
        {
            _entityMovement = new EntityMovement(_movementData);
            _input = new Input();
            _input.Enable();
        }

        private void Update()
        {
            _direction = GetDirection(PlatformType.Desctop);

            _animation.SetAnimationFloat(AnimationParamType.Speed, GetSpeed(_direction));

            //foreach (var hotKey in _hotKeys)
            //{
            //    if (Input.GetKeyDown(hotKey.KeyCode))
            //    {
            //        OnHotKeyPress?.Invoke(hotKey.Action);
            //    }
            //}
        }

        private void FixedUpdate()
        {
            _entityMovement.Move(_direction);
            _entityMovement.Rotate(_movementData.Transform, _direction);

            _mapManage.GetNearStrcture(_movementData.Rigidbody.position);
        }

        public Vector2 GetDirection(PlatformType type)
        {
            switch (type)
            {
                case PlatformType.Desctop:
                    return _input.PlayerInput.Move.ReadValue<Vector2>();
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