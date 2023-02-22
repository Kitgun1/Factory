using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Factory
{
    public class CreatorPerforms : MonoBehaviour
    {
        [SerializeField] private Transform _creationPoint;

        public event UnityAction<Product> Spawn;
        
        private Creator _creator;
        private IEnumerator _creatorEnumerator = null;

        private void Start()
        {
            _creator = GetComponent<Creator>();

            StartCreatorRoutine();
        }

        public void StartCreatorRoutine()
        {
            if (_creatorEnumerator != null)
            {
                StopCreatorRoutine();
            }
            _creatorEnumerator = CreatorRoutine();
            StartCoroutine(_creatorEnumerator);
        }

        public void StopCreatorRoutine()
        {
            if (_creatorEnumerator == null) return;
            StopCoroutine(_creatorEnumerator);
            _creatorEnumerator = null;
        }

        private IEnumerator CreatorRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_creator.GetCurrentSpeed());
                var product = Instantiate(_creator.GetCurrentTemplate().GameObject, _creationPoint.position, Quaternion.identity, transform);
                Spawn?.Invoke(product);
            }
        }
    }
}