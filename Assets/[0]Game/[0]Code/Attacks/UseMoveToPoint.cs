using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class UseMoveToPoint : MonoBehaviour
    {
        [SerializeField]
        private bool _isStart = true;
                
        [SerializeField]
        private Transform _transform;
        
        [SerializeField]
        private Transform _point;
        
        [SerializeField]
        private float _moveSpeed = 3;

        [SerializeField]
        private UnityEvent _event;

        public void Start()
        {
            if (!_isStart)
                return;

            if (_transform == null)
                _transform = transform;
            
            Use();
        }
        
        public void Use()
        {
            StartCoroutine(AwaitMove());
        }

        private IEnumerator AwaitMove()
        {
            while (_transform.position != _point.position)
            {
                _transform.position = Vector2.MoveTowards(_transform.position, _point.position, Time.deltaTime * _moveSpeed);
                yield return null;
            }
            
            _event.Invoke();
        }
    }
}