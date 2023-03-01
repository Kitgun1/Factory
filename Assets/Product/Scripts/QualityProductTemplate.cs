using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(menuName = "Products/Quality", fileName = "Quality", order = 2)]
    public class QualityProductTemplate : ProductTemplate
    {
        [SerializeField] private QualityProductInfo[] _levelInfo;

        public QualityProductInfo[] LevelInfo => _levelInfo;
        public override int MaxLevel() => _levelInfo.Length - 1;

        public override double Price(int level)
        {
            if (level > 0 && level < MaxLevel())
                return _levelInfo[level].Price;
            else
                return 0;
        }
    }
}