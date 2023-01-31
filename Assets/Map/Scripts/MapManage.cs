using DG.Tweening;
using UnityEngine;

namespace Shoping
{
    public class MapManage : MonoBehaviour
    {
        [Min(1), SerializeField] private Vector2Int _sizeMap = Vector2Int.one;
        [Min(0.01f), SerializeField] private float _widthCell = 1f;
        [SerializeField] private GameObject _plane;
        [SerializeField] private UIMap _uIMapManager;

        private IInsertable _map;

        private void Awake()
        {
            _map = new Map(_sizeMap.x, _sizeMap.y);
            var plane = Instantiate(_plane, Vector3.zero, Quaternion.identity, transform);
            plane.transform.localScale = new Vector3(_widthCell * _sizeMap.x, .25f, _widthCell * _sizeMap.y);
            plane.GetComponent<MeshRenderer>().material.DOTiling(new Vector2(_sizeMap.x, _sizeMap.y), 0f);
            _uIMapManager.SetMap(_sizeMap.x,_sizeMap.y);
        }
    }
}