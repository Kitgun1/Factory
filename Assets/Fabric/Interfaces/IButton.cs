using UnityEngine.Events;

namespace Shoping
{
	public interface IButton
	{
		public event UnityAction<Wallet> OnPressed;
	}
}