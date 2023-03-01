using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
	public class QualityUpgrader : Interacter
	{
        [SerializeField] private List<float> QualityImprovementAmount;

        protected override void Action(Product product)
        {
            if (product is IQuality quality)
                quality.SetQuality(QualityImprovementAmount[Level]);
            if (product is IUpgradeable upgradeable)
                upgradeable.TryUpgrade();
        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
            LimitModifer(QualityImprovementAmount);
        }
    }
}