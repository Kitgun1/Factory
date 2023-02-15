using UnityEngine;
using System.Collections.Generic;

namespace Factory
{
    public class Conveyor : Item
    {
        [SerializeField] private List<float> _levelsSpeed = new List<float>();

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

        private void OnValidate()
        {
            if (_levelsSpeed.Count > MaxLevel)
            {
                _levelsSpeed.RemoveRange(MaxLevel, _levelsSpeed.Count - MaxLevel);
            }
            else if (_levelsSpeed.Count < MaxLevel)
            {
                List<float> tempLevels = new List<float>(_levelsSpeed);
                for (int i = 0; i < MaxLevel - _levelsSpeed.Count; i++)
                {
                    tempLevels.Add(0f);
                }
                _levelsSpeed = new List<float>(tempLevels);
            }
        }
    }
}