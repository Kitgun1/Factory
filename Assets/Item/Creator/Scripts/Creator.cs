using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
    [RequireComponent(typeof(CreatorPerforms))]
    public class Creator : Item
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private List<Material> _materials;
        [SerializeField] private List<float> _creationsSpeed;
        [SerializeField] private GameObject _object;

        private CreatorPerforms _performs;

        public float GetCurrentSpeed() => _creationsSpeed[Level];
        public GameObject GetCurrentObject() => _object;

        private void Start()
        {
            _performs = GetComponent<CreatorPerforms>();
            SetUpgrade();
        }

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
            SetUpgrade(Level);
        }

        private void SetUpgrade(int level = -1)
        {
            if (level < 0)
                level = 0;

            _renderer.material = _materials[level];
        }

        private void OnValidate()
        {
            if (_materials.Count > MaxLevel)
            {
                _materials.RemoveRange(MaxLevel, _materials.Count - MaxLevel);
            }
            else if (_materials.Count < MaxLevel)
            {
                List<Material> tempLevels = new List<Material>(_materials);
                for (int i = 0; i < MaxLevel - _materials.Count; i++)
                {
                    tempLevels.Add(null);
                }
                _materials = new List<Material>(tempLevels);
            }

            if (_creationsSpeed.Count > MaxLevel)
            {
                _creationsSpeed.RemoveRange(MaxLevel, _creationsSpeed.Count - MaxLevel);
            }
            else if (_creationsSpeed.Count < MaxLevel)
            {
                List<float> tempLevels = new List<float>(_creationsSpeed);
                for (int i = 0; i < MaxLevel - _creationsSpeed.Count; i++)
                {
                    tempLevels.Add(0f);
                }
                _creationsSpeed = new List<float>(tempLevels);
            }
        }
    }
}