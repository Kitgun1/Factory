using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[RequireComponent(typeof(Rigidbody))]
public class EntityFollow : MonoBehaviour
{
    [SerializeField] private EntityFollowData _followData;

    private Rigidbody _body;
    private Vector3 _velocity = Vector3.zero;
    private float _multiplySpeed = 100;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        _body.useGravity = false;
    }

    private void Update()
    {
        Move();
        Rotation();
    }

    private void Move()
    {
        Vector3 targetVelocity = (_followData.Target.position + _followData.Distance - transform.position) * _followData.SpeedMovementFollow * _multiplySpeed * Time.deltaTime;
        _body.velocity = Vector3.SmoothDamp(_body.velocity, targetVelocity, ref _velocity, _followData.SmoothSpeedMovement);
    }

    private void Rotation()
    {
        _body.rotation = Quaternion.Slerp(_body.rotation, Quaternion.Euler(_followData.Rotation), _followData.InterpolationRation);
    }
}

[System.Serializable]
public struct EntityFollowData
{
    public Transform Target;
    public Vector3 Distance;
    public Vector3 Rotation;
    public float SpeedMovementFollow;
    [Range(0f, 1f)] public float InterpolationRation;
    [Range(0f, 0.1f)] public float SmoothSpeedMovement;
}