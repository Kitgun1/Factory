using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
	public class QualityUpgrader : Interacter
	{
        [SerializeField] private List<float> QualityImprovementAmount;

        protected override void Action(Product product)
        {
            if (product is IQuality upgradeable)
                upgradeable.SetQuality(QualityImprovementAmount[Level]);
        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
            LimitModifer(MaxProductCount);
            LimitModifer(QualityImprovementAmount);
        }
    }
}