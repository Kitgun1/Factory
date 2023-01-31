using UnityEngine;

namespace Shoping
{
    public interface IInsertable
    {
        public void InsertItem(Item item, int x, int y, float rotationY = 0, Transform parent = null);
    }
}