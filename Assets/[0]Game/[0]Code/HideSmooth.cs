using System;
using System.Collections;
using Super_Auto_Mobs;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HideSmooth : MonoBehaviour
    {
        [SerializeField]
        private bool _isStart = true;

        [SerializeField] 
        private float _duration = 1;

        [SerializeField] 
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private UnityEvent _event;
        
        public void Start()
        {
            if (!_isStart)
                return;
            
            Use();
        }
        
        public void Use()
        {
            StartCoroutine(AwaitHide());
        }
        
        public IEnumerator AwaitHide()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            
            var alpha = _spriteRenderer.color.a;
            
            while (alpha != 0)
            {
                alpha = _spriteRenderer.color.a;
                alpha -= Time.deltaTime / _duration;
                _spriteRenderer.color = _spriteRenderer.color.SetA(alpha);
                yield return null;
            }
            
            _event.Invoke();
        }

        public void SetDuration(float value)
        {
            _duration = value;
        }
    }
}