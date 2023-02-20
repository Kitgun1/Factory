using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(menuName = "Products/Visual", fileName = "Visual", order = 3)]
    public class VisualProductTemplate : ProductTemplate
    {
        [SerializeField] private VisualProductInfo[] _levelInfo;

        public VisualProductInfo[] LevelInfo => _levelInfo;
        public override int MaxLevel() => _levelInfo.Length - 1;
    }
}