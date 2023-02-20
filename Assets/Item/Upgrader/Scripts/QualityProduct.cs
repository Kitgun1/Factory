using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(menuName = "Products/Quality", fileName = "Quality", order = 2)]
    public class QualityProduct : Product
    {
        [SerializeField] private QualityProductLevelInfo[] _levelInfo;
        public override int MaxLevel() => _levelInfo.Length - 1;
    }
}