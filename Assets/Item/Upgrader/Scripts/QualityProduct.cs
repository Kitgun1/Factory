namespace Factory
{
	public class QualityProduct : Product
	{
        public float Quality { get; private set; }

        QualityProductTemplate _template;

        public override void Init(ProductTemplate template)
        {
            _template = template as QualityProductTemplate;
            base.Init(template);
        }

        public override void UpdateInfo()
        {
            Quality = _template.LevelInfo[Level].Quality;
        }
    }
}