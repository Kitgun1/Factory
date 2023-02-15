using System;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
	[Serializable]
	public struct CreatorItemData
	{
		public MeshRenderer Renderer;
		public List<Texture> TextureList;
	}
}