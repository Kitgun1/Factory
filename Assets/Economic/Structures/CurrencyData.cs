using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shoping
{
	[Serializable]
	public struct CurrencyData
	{
		public string CurrencyName;
		[Space(5)]
		public Image Image;
        [ShowAssetPreview(32,32)] public Sprite Sprite;
		[Space(2)]
		public TMP_Text BalanceText;
		public float BalanceValue;
	}
}