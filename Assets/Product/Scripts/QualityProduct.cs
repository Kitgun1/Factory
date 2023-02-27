namespace Factory
{
	public class QualityProduct : Product, IQuality
	{
        public float Quality { get; private set; }

        private QualityProductTemplate _template;

        private const float _maxQuality = 1;

        public override void Init(ProductTemplate template, bool cloned)
        {
            _template = template as QualityProductTemplate;
            base.Init(template, cloned);
        }

        protected override void UpdateInfo()
        {

        }

        public void SetQuality(float value)
        {
            if (value > 0 && value < _maxQuality)
            Quality = value;
        }
    }
}