using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shoping
{
    public class MapManage : MonoBehaviour
    {
        [Header("Property")]
        [Min(1), SerializeField] private Vector2Int _sizeMap = Vector2Int.one;
        [Min(0.01f), SerializeField] private float _widthCell = 1f;
        [SerializeField] private int _cellSizeUI = 150;
        [SerializeField] private List<CellInfoData> _cellInfo = new List<CellInfoData>();

        [Space(10f), Header("Link")]
        [SerializeField] private GameObject _cellUITemplate;
        [SerializeField] private ActionPanel _actionPanel;
        [SerializeField] private Transform _parentUICell;
        [SerializeField] private GameObject _plane;

        private Dictionary<Vector2Int, ConnectionData<GameObject, Cell>> _cells = new Dictionary<Vector2Int, ConnectionData<GameObject, Cell>>();
        private GridLayoutGroup _layoutGroup;
        private IInsertable _map;

        private void Awake()
        {
            _layoutGroup = _parentUICell.GetComponent<GridLayoutGroup>();
            InitMap();
        }

        private void InitMap()
        {
            _map = new Map(_sizeMap.x, _sizeMap.y);
            var plane = Instantiate(_plane, Vector3.zero, Quaternion.identity, transform);
            plane.transform.localScale = new Vector3(_widthCell * _sizeMap.x, 0.5f, _widthCell * _sizeMap.y);
            plane.transform.position -= new Vector3(0f, 0.25f, 0f);
            plane.GetComponent<MeshRenderer>().material.DOTiling(new Vector2(_sizeMap.x, _sizeMap.y), 0f);

            _layoutGroup.constraintCount = _sizeMap.x;
            _layoutGroup.cellSize = new Vector2(_cellSizeUI, _cellSizeUI);
            for (int y = 0; y < _sizeMap.y; y++)
            {
                for (int x = 0; x < _sizeMap.x; x++)
                {
                    var cellUI = Instantiate(_cellUITemplate, _parentUICell).GetComponent<Cell>();
                    var gameObject = Instantiate(new GameObject("Cell Point"), new Vector3(x, 0f, y), Quaternion.identity, transform);

                    var connection = new ConnectionData<GameObject, Cell>(gameObject, cellUI);

                    // Set property UI cell
                    cellUI.Button.onClick.AddListener(() => OnClickCell(connection));
                    cellUI.SetCell(null,null);

                    _cells.Add(new Vector2Int(x, y), connection);
                }
            }
        }

        public CellInfoData GetCellInfo(string name)
        {
            foreach (var cell in _cellInfo)
            {
                if (name == cell.Name) return cell;
            }

            throw new MissingReferenceException(name);
        }

        private void OnClickCell(ConnectionData<GameObject, Cell> connection)
        {
            _actionPanel.SetInteractables(connection.TValue2.CellType);
            _actionPanel.SetCurrentCell(connection);
        }
    }
}