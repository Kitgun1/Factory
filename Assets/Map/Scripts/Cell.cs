using UnityEngine;
using UnityEngine.UI;

namespace Shoping
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Image IconState;

        public Button Button;
        public Vector2Int Position;
        public GameObject GameObject3D;
        public ItemType CellType;

        public void SetCell(Sprite sprite, GameObject gameObject)
        {
            IconState.sprite = sprite;
            GameObject3D = gameObject;
            SetRotation(0f);
        }

        public void AddRotation(float value)
        {
            IconState.rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, value) + IconState.rectTransform.rotation.eulerAngles);
            GameObject3D.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, value) + GameObject3D.transform.rotation.eulerAngles);
        }

        public void SetRotation(float value)
        {
            IconState.rectTransform.rotation = Quaternion.Euler(0f, 0f, value);
            GameObject3D.transform.rotation = Quaternion.Euler(0f, 0f, value);
        }
    }
}