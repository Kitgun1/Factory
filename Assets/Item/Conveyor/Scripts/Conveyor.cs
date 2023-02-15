using UnityEngine;
using System.Collections.Generic;

namespace Factory
{
    public class Conveyor : Item
    {
        private void OnEnable()
        {
            OnUpgrade += OnUpgraded;
        }

        private void OnDisable()
        {
            OnUpgrade -= OnUpgraded;
        }

        private void OnUpgraded()
        {

        }
    }
}