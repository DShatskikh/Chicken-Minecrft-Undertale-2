using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game
{
    public class TypingText : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text _label;
        
        [SerializeField, TextArea]
        private string _text;

        [SerializeField] 
        private AudioSource _audioSource;

        private Coroutine _coroutine;
        public bool IsTyping { get; set; }

        private void OnEnable()
        {
            Write(_text);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel") && _coroutine != null)
            {
                IsTyping = false;
                StopCoroutine(_coroutine);
                _label.text = _text;
            }
        }

        public void Write(string text)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _text = text;
            _coroutine = StartCoroutine(AwaitWrite());
        }
        
        private IEnumerator AwaitWrite()
        {
            IsTyping = true;
            _label.text = "";

            var currentText = "";
            int _countSymbol = 0;

            while (_countSymbol != _text.Length)
            {
                currentText += _text[_countSymbol];
                
                if (_audioSource)
                    _audioSource.Play();
                
                _label.text = currentText;

                for (int i = currentText.Length; i < _text.Length; i++)
                {
                    _label.text += ' ';
                }
                
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }

            IsTyping = false;
        }
    }
}