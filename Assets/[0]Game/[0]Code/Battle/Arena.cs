using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class Arena : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _defaultSize;
        
        [SerializeField]
        private Vector2 _infoSize;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public IEnumerator AwaitToDefault()
        {
            yield return AwaitToSize(_defaultSize);
        }
        
        public IEnumerator AwaitToInfo()
        {
            yield return AwaitToSize(_infoSize);
        }
        
        public IEnumerator AwaitToSize(Vector2 target)
        {
            while (_spriteRenderer.size != target)
            {
                Vector2.MoveTowards(_spriteRenderer.size, target, Time.deltaTime);
                yield return null;
            }
        }
    }
}