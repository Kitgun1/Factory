using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shoping.A
{
    [Serializable]
    public struct ItemUpgradeData
    {
        public float Price;
        public CurrencyType CurrencyType;
        [Min(0)] public float IncomeAmount;
        public Sprite Icon;
        public List<GameObject> ActiveObjects;
        public List<GameObject> InactiveObjects;
    }
}