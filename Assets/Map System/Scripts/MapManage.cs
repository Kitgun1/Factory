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

        private void Start()
        {
        }

        private void Init()
        {
            _map = new Map(_defaultItem, _size.x, _size.y);
            var platform = Instantiate(_platform, transform.position, Quaternion.identity, transform);
            platform.transform.localScale = new Vector2(_size.x * _sizeCell, _size.y * _sizeCell);
            platform.GetComponent<MeshRenderer>().material.DOTiling(_size, 0.2f);
        }

        public void GetNearCell(Vector3 worldPosition)
        {

        }
    }
}