using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Factory
{
	[Serializable]
	public struct CurrencyData
	{
		public CurrencyType CurrencyType;

		[Space(5)]
		public Image Image;
        [ShowAssetPreview(32,32)] public Sprite Sprite;

		[Space(2)]
		public TMP_Text BalanceText;
		public float BalanceValue;
		public float BalanceDefaultValue;
	}
}