using UnityEngine;

namespace Factory
{
    public class Product : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private MeshFilter _filter;

        public int Level { get; private set; }
        public float Quality { get; private set; }
        private const int _maxQuality = 100;

        public void ChangeVisual(ProductLevelInfo info)
        {
            _filter.mesh = info.Mesh;
            _renderer.material = info.Material;
        }

        public void TryUpgradeLevel(int maxValue, float quality)
        {
            if (Level < maxValue)
                Level++;
            if (quality < _maxQuality)
                Quality = quality;
        }
    }
}