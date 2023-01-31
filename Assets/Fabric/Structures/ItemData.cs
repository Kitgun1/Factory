using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shoping.A
{
    [Serializable]
    public struct ItemData
    {
        [Header("Upgrade Property")]
        public TMP_Text PriceText;
        public Image PriceIconImage;
        public GameObject BuyedObject;
        public PhysicsButton ButtonUpgrade;
        public List<ItemUpgradeData> Upgrades;

        [Space(5), Header("Income Property")]
        public TMP_Text IncomeText;
        public Image IncomeIconImage;
        public IncomeButton ButtonIncome;
        public CurrencyType IncomeCurrencyType;
    }
}