using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
    [RequireComponent(typeof(CreatorPerforms))]
    public class Creator : Structure, ITurn
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private List<Material> _materials;
        [SerializeField] private ProductTemplate _template;
        [SerializeField] private float _rotationDuration;

        private CreatorPerforms _performs;

        public float GetCurrentSpeed() => Modifer[Level];
        public ProductTemplate GetCurrentTemplate() => _template;

        private void Start()
        {
            SetUpgrade();
        }

        private void OnEnable()
        {
            _performs = GetComponent<CreatorPerforms>();
            OnUpgrade += OnUpgraded;
            _performs.Spawn += OnProductSpawned;
        }

        private void OnDisable()
        {
            OnUpgrade -= OnUpgraded;
            _performs.Spawn -= OnProductSpawned;
        }

        private void OnUpgraded()
        {
            SetUpgrade(Level);
        }

        private void OnProductSpawned(Product product)
        {
            product.Init(_template);
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

            LimitModifer();
        }

        public void Rotate(Transform transform)
        {
            EntityTurner.Action(transform, _rotationDuration);
        }
    }
}