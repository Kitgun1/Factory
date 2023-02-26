using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class MoverManage : MonoBehaviour
    {
        private List<ProductMoveData> _productsMove = new List<ProductMoveData>();
        private List<ProductMoveData> _productsRepeat = new List<ProductMoveData>();

        public static MoverManage Instance = null;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance = this) Destroy(gameObject);
        }

        private void Update()
        {
            foreach (var productMove in _productsMove)
            {
                Conveyor conveyor = productMove.Conveyor;
                Vector2 targetPosition = new Vector2(conveyor.CurrentDirection().x, conveyor.CurrentDirection().y);//GetTargetPosition(conveyor, currentPosition);
                Vector3 velocity = new Vector3(targetPosition.x, 0, targetPosition.y) * conveyor.CurrentSpeed();

                productMove.Product.Rigidbody.velocity = velocity;
            }
        }

        public void AddProductMove(Product product, Conveyor conveyor)
        {
            ProductMoveData productMove = new ProductMoveData(product, conveyor);

            bool isRepeat = false;
            for (int i = 0; i < _productsMove.Count; i++)
            {
                if (_productsMove[i].Product == productMove.Product)
                {
                    _productsMove[i] = productMove;
                    _productsRepeat.Add(productMove);
                    isRepeat = true;
                    break;
                }
            }
            if (isRepeat == false)
            {
                _productsMove.Add(productMove);
            }
        }

        public bool TryRemoveProductMove(Product product)
        {
            for (int i = 0; i < _productsMove.Count; i++)
            {
                if (_productsMove[i].Product == product)
                {
                    bool isRepeat = false;
                    for (int j = 0; j < _productsRepeat.Count; j++)
                    {
                        if (_productsRepeat[j].Product == product)
                        {
                            _productsRepeat.RemoveAt(j);
                            isRepeat = true;
                            break;
                        }
                    }
                    if (isRepeat == false)
                    {
                        _productsMove.RemoveAt(i);
                    }
                    break;
                }
            }
            return false;
        }
    }
}