using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Factory
{
    public class UIConstructionController : MonoBehaviour
    {
        [BoxGroup("Parents"), SerializeField] private Transform _modifersParent;
        [BoxGroup("Parents"), SerializeField] private Transform _actionsParent;
        [BoxGroup("Parents"), SerializeField] private Transform _constructionsParent;

        [BoxGroup("Templates"), SerializeField] private UIModifer _modiferTemplate;

        [BoxGroup("Tittle"), SerializeField] private Image _structureIcon;
        [BoxGroup("Tittle"), SerializeField] private TMP_Text _structureName;
        [BoxGroup("Tittle"), SerializeField] private List<UIStructureData> _structureDatas;

        private List<UIModifer> _modifers = new List<UIModifer>();

        public void AddModifer(string name, string description, float priceUpgrade, SliderData<float> slider)
        {
            var modifer = Instantiate(_modiferTemplate, _modifersParent);
            modifer.SetParams(name, description, priceUpgrade, slider);
            _modifers.Add(modifer);
        }
    }
}