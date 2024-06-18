using System;
using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class Select : MonoBehaviour
    {
        [SerializeField]
        private Button _yesButton;
        
        [SerializeField]
        private Button _noButton;
        
        [SerializeField]
        private TMP_Text _label;
        
        private string _text;
        private Coroutine _coroutine;

        private Action _yesAction;
        private Action _noAction;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                SelectFalse();
            }
        }

        public void Show(string text, Action yesAction, Action noAction)
        {
            gameObject.SetActive(true);
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            GameData.Character.enabled = false;

            _yesAction = yesAction;
            _noAction = noAction;
            
            _yesButton.onClick.AddListener(SelectTrue);
            SignalBus.OnSubmit += SelectTrue;
            
            _noButton.onClick.AddListener(SelectFalse);

            _text = text;
            
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(TypeText());
        }

        private IEnumerator TypeText()
        {
            int _countSymbol = 0;
            string currentText = "";

            while (_countSymbol != _text.Length)
            {
                currentText += _text[_countSymbol];
                SetText(currentText);
                GameData.TextAudioSource.Play();
                yield return new WaitForSeconds(0.05f);
                _countSymbol++;
            }
        }

        private void SelectTrue()
        {
            SignalBus.OnSubmit = null;
            Close();
            _yesAction?.Invoke();
        }
        
        private void SelectFalse()
        {
            SignalBus.OnSubmit = null;
            Close();
            _noAction?.Invoke();
        }
        
        private void Close()
        {
            GameData.EffectAudioSource.clip = GameData.ClickSound;
            GameData.EffectAudioSource.Play();
            gameObject.SetActive(false);
            GameData.Character.enabled = true;
            
            _yesButton.onClick.RemoveAllListeners();
            _noButton.onClick.RemoveAllListeners();
        }

        private void SetText(string text)
        {
            _label.text = text;
        }
    }
}