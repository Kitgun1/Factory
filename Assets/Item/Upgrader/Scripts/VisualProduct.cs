using UnityEngine;

namespace Factory
{
	public class VisualProduct : Product
	{
		[SerializeField] private MeshFilter _filter;
		[SerializeField] private MeshRenderer _renderer;

        private VisualProductTemplate _template;

        public override void Init(ProductTemplate template)
        {
            _template = template as VisualProductTemplate;
            base.Init(template);
        }

        public override void UpdateInfo()
        {
            VisualProductInfo info = _template.LevelInfo[Level];

            _filter.mesh = info.Mesh;
            _renderer.material = info.Material;
        }
    }
}