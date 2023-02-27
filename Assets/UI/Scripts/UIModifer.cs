using KiMath;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Factory
{
    public class UIModifer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _priceUpgrade;
        [SerializeField] private Slider _currentLevel;
        [SerializeField] private Button _upgrade;

        public void AddListener(UnityAction call) => _upgrade.onClick.AddListener(call);
        public void RemoveAllListeners() => _upgrade.onClick.RemoveAllListeners();

        public void SetParams(string name, string description, float priceUpgrade, SliderData<float> slider)
        {
            _name.text = name;
            _description.text = description;
            SetSlider(slider);
            SetPrice(priceUpgrade);
        }

        public void SetSlider(SliderData<float> slider)
        {
            _currentLevel.minValue = slider.MinValue;
            _currentLevel.maxValue = slider.MaxValue;
            _currentLevel.value = slider.Value;
        }

        public void SetPrice(float priceUpgrade)
        {
            _priceUpgrade.text = MathK.ToString(priceUpgrade);
        }
    }
}