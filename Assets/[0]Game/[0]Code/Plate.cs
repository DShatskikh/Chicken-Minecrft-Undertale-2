using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class Plate : MonoBehaviour
    {
        [SerializeField] 
        private Sprite _pressedSprite;

        [SerializeField] 
        private Sprite _notPressedSprite;

        [SerializeField]
        private Color _pressedTextColor;
        
        [SerializeField]
        private Color _notPressedTextColor;
        
        [SerializeField]
        private TMP_Text _label;
        
        private SpriteRenderer _spriteRenderer;
        private PlaySound _playSound;
        
        public int Number;
        public bool IsPress;
        public event Action<Plate> OnPress;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playSound = GetComponentInChildren<PlaySound>();
        }

        private void Start()
        {
            _label.text = Number.ToString();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Character character) && !IsPress)
            {
                Press();
                _playSound.Play();
            }
        }

        public void Press()
        {
            _spriteRenderer.sprite = _pressedSprite;
            _label.color = _pressedTextColor;
            IsPress = true;
            
            OnPress?.Invoke(this);
        }

        public void NotPress()
        {
            _spriteRenderer.sprite = _notPressedSprite;
            _label.color = _notPressedTextColor;
            IsPress = false;
        }
    }
}