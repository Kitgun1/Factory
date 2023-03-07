using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Factory
{
    public class UIConstructionController : MonoBehaviour
    {
        [BoxGroup("Panels"), SerializeField] private GameObject _structureProperty;

        [BoxGroup("Parents"), SerializeField] private Transform _modifersParent;
        [BoxGroup("Parents"), SerializeField] private Transform _actionsParent;
        [BoxGroup("Parents"), SerializeField] private Transform _constructionsParent;

        [BoxGroup("Templates"), SerializeField] private UIModifer _modiferTemplate;

        [BoxGroup("Tittle"), SerializeField] private Image _structureIcon;
        [BoxGroup("Tittle"), SerializeField] private TMP_Text _structureName;
        [BoxGroup("Tittle"), SerializeField] private List<UIStructureData> _structureDatas;

        [SerializeField] private Input _input;

        private List<UIModifer> _modifers = new List<UIModifer>();

        private void Awake()
        {
            _input = new Input();
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.PlayerInput.CellMenu.performed += ctx => OpenStructurePanel();
            _input.PlayerInput.CloseAnyMenu.performed += ctx => CloseStructurePanel();
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.PlayerInput.CellMenu.performed -= ctx => OpenStructurePanel();
            _input.PlayerInput.CloseAnyMenu.performed -= ctx => CloseStructurePanel();
        }

        private void OpenStructurePanel()
        {
            _structureProperty.SetActive(!_structureProperty.activeSelf);
        }

        private void CloseStructurePanel()
        {
            _structureProperty.SetActive(false);
        }

        public void AddModifer(string name, string description, float priceUpgrade, SliderData<float> slider)
        {
            var modifer = Instantiate(_modiferTemplate, _modifersParent);
            modifer.SetParams(name, description, priceUpgrade, slider);
            _modifers.Add(modifer);
        }
    }
}