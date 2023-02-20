using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(menuName = "Products/Visual", fileName = "Visual", order = 3)]
    public class VusialProduct : Product
    {
        [SerializeField] private VisualProductLevelInfo[] _levelInfo;

        public override int MaxLevel() => _levelInfo.Length - 1;
    }
}