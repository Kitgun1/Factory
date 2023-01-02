using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class EntityFollow : MonoBehaviour
{
    [SerializeField] private EntityFollowData _followData;

    private Rigidbody _body;

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
        _body.transform.position = _followData.Target.position + _followData.Distance;
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
    [Range(0f, 1f)] public float InterpolationRation;
}