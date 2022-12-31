using UnityEngine;

namespace Shoping
{
	public class Item : MonoBehaviour, IItem
	{
		[SerializeField] private ItemInfoData _itemInfoData;

        private void OnEnable()
        {
            _itemInfoData.Button.OnPressed += OnPressed;
        }

        private void OnDisable()
        {
            
        }

        public void OnPressed(Wallet wallet)
        {
        }
    }
}