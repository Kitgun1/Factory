using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shoping
{
	[Serializable]
	public struct ButtonGroupData
	{
		public GameObject GroupParent;
		public List<Button> Buttons;
	}
}