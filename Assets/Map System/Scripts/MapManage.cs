using DG.Tweening;
using UnityEngine;

namespace Factory
{
    public class MapManage : MonoBehaviour
    {
        [SerializeField] private GameObject _platform;
        [Min(1), SerializeField] private Vector2Int _size = Vector2Int.one;

        private Map _map;

        private Vector3 _nearCell = Vector3.zero;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _map = new Map(_size.x, _size.y);

            Vector3 offset = new Vector3(0f, -0.5f, 0f);
            if (_size.x % 2 == 0)
                offset.x = 0.5f;
            if (_size.y % 2 == 0)
                offset.z = 0.5f;

            var platform = Instantiate(_platform, transform.position + offset, Quaternion.identity, transform);
            platform.transform.localScale = new Vector3(_size.x, 1f, _size.y);
            platform.GetComponent<MeshRenderer>().material.DOTiling(_size, 0.2f);
        }

        public void GetNearStrcture(Vector3 worldPosition)
        {
            var strcture = _map.GetNearStructure(new Vector2(worldPosition.x, worldPosition.z), _size, out Vector2 worldPositionCell);
            if (strcture == null) return;
            _nearCell = new Vector3(worldPositionCell.x, 0f, worldPositionCell.y);

            //if (strcture == null)
            //{
            //    var structure = Instantiate(new structure, _nearCell, Quaternion.identity, transform);
            //    strcture.TrySetItem(structure);
            //}
        }

        public void GetStructure(int x, int y)
        {
            _map.GetStructure(x, y);
        }

        public bool TryCreateStructure(Structure structure, int x, int y)
        {
            if (_map.GetStructure(x, y) != null) return false;

            structure = Instantiate(structure, _nearCell, Quaternion.identity, transform);
            _map.SetStructure(structure, x, y);
            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(_nearCell, Vector3.one * 0.2f);
        }
    }
}