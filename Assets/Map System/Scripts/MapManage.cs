using DG.Tweening;
using UnityEngine;

namespace Factory
{
    public class MapManage : MonoBehaviour
    {
        [SerializeField] private GameObject _platform;
        [Min(1), SerializeField] private Vector2Int _size = Vector2Int.one;
        [SerializeField] private Structure _defaultItem = null;

        private Map _map;

        private Vector3 _nearCell = Vector3.zero;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _map = new Map(_defaultItem, _size.x, _size.y);

            Vector3 offset = new Vector3(0f, -0.5f, 0f);
            if (_size.x % 2 == 0)
                offset.x = 0.5f;
            if (_size.y % 2 == 0)
                offset.z = 0.5f;

            var platform = Instantiate(_platform, transform.position + offset, Quaternion.identity, transform);
            platform.transform.localScale = new Vector3(_size.x, 1f, _size.y);
            platform.GetComponent<MeshRenderer>().material.DOTiling(_size, 0.2f);
        }

        public void GetNearCell(Vector3 worldPosition)
        {
            var cell = _map.GetNearCell(new Vector2(worldPosition.x, worldPosition.z), _size, out Vector2 worldPositionCell);
            if (cell == null) return;
            _nearCell = new Vector3(worldPositionCell.x, 0f, worldPositionCell.y);

            if (cell.GetItem() == null && _defaultItem != null)
            {
                Instantiate(_defaultItem, _nearCell, Quaternion.identity, transform);
                cell.TrySetItem(_defaultItem);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(_nearCell, Vector3.one * 0.2f);
        }
    }
}