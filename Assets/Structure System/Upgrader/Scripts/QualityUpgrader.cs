using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
	public class QualityUpgrader : Interacter
	{
        [SerializeField] private List<float> QualityImprovementAmount;

        private void OnEnable()
        {
            Init();
            OnProductInside += OnProductGet;
        }

        private void OnDisable()
        {
            OnProductInside -= OnProductGet;
        }

        private void OnValidate()
        {
            LimitList(SpeedTickModifers, Level);
            LimitList(QualityImprovementAmount, Level);
        }

        protected override void Action(Product product)
        {
            if (product is IQuality quality)
                quality.SetQuality(QualityImprovementAmount[Level]);
            if (product is IUpgradeable upgradeable)
                upgradeable.TryUpgrade();
        }
    }
}