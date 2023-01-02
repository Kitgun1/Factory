using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Shoping
{
	[Serializable]
	public struct ItemInfoData
	{
		public Button Button;
		public GameObject Table;
		public TMP_Text PriceText;
		public CurrencyType CurrencyType;
		public float PriceBuy;
		public List<ItemUpgradeData> Upgrades;
	}
}