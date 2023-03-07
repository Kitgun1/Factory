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
            ProductInside += OnProductGet;
        }

        private void OnDisable()
        {
            ProductInside -= OnProductGet;
        }

        private void OnValidate()
        {
            SpeedTickModifers = LimitList(SpeedTickModifers, Level);
            QualityImprovementAmount = LimitList(QualityImprovementAmount, Level);
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