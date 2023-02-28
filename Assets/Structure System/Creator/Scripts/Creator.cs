using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using KiMath;

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
            product.Init(_template, false);
        }

        private void SetUpgrade(int level = -1)
        {
            if (level < 0)
                level = 0;

            _renderer.material = _materials[level];
        }

        private void OnValidate()
        {
            LimitModifer(Modifer);
            LimitModifer(_materials);
        }

        public void Rotate(Transform transform)
        {
            EntityTurner.Action(transform, _rotationDuration);
        }
    }
}