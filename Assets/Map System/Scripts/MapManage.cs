using DG.Tweening;
using KiMath;
using UnityEngine;

namespace Factory
{
    public class MapManage : MonoBehaviour
    {
        [SerializeField] private GameObject _platform;
        [Min(1), SerializeField] private Vector2Int _size = Vector2Int.one;

        [SerializeField] private Structure _structure;

        private Map _map;
        private Vector3 _nearCell = Vector3.zero;
        private Vector2Int _currentPositionInMap;

        public static MapManage Instance = null;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance = this) Destroy(gameObject);
        }

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            if (_structure == null) return;

            TryCreateStructure(_structure, _currentPositionInMap.x, _currentPositionInMap.y);
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
            var strcture = _map.GetNearStructure(new Vector2(worldPosition.x, worldPosition.z), _size, out Vector2 worldPositionCell, out _currentPositionInMap);
            if (_currentPositionInMap.x == -1 || _currentPositionInMap.y == -1) return;
            _nearCell = new Vector3(worldPositionCell.x, 0.5f, worldPositionCell.y);

            //if (strcture == null)
            //{
            //    var structure = Instantiate(new structure, _nearCell, Quaternion.identity, transform);
            //    strcture.TrySetItem(structure);
            //}
        }

        public Structure GetStructure(int x, int y)
        {
            return _map.GetStructure(x, y);
        }

        public bool TryCreateStructure(Structure structure, int x, int y)
        {
            if (_map.GetStructure(x, y) != null) return false;

            if (_map.CheckPointInBorder(x, y))
            {
                structure = Instantiate(structure, _nearCell + new Vector3(0f, -0.5f, 0), Quaternion.identity, transform);
                _map.SetStructure(structure, x, y);
                return true;
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(_nearCell - new Vector3(0f, 0.4f, 0f), new Vector3(1f, 0.2f, 1f));
        }
    }
}