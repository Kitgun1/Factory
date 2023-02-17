using DG.Tweening;
using UnityEngine;

namespace Factory
{
    public class MapManage : MonoBehaviour
    {
        [SerializeField] private GameObject _platform;
        [Min(1), SerializeField] private Vector2Int _size = Vector2Int.one;
        [SerializeField] private float _sizeCell = 1f;
        [SerializeField] private Item _defaultItem = null;

        private Map _map;

        private Vector3 _nearCell = Vector3.zero;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _map = new Map(_defaultItem, _size.x, _size.y);
            var platform = Instantiate(_platform, transform.position + Vector3.down, Quaternion.identity, transform);
            platform.transform.localScale = new Vector3(_size.x * _sizeCell, 1f, _size.y * _sizeCell);
            platform.GetComponent<MeshRenderer>().material.DOTiling(_size, 0.2f);
        }

        public void GetNearCell(Vector3 worldPosition)
        {
            _map.GetNearCell(worldPosition, _sizeCell, _size, out Vector2Int pos);
            _nearCell = new Vector3(pos.x, 1f, pos.y);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(_nearCell, Vector3.one * 0.2f);
        }
    }
}