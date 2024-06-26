﻿using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class KeyUseButton : MonoBehaviour
    {
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            SignalBus.OnSubmit += OnSubmit;
        }

        private void OnSubmit()
        {
            SignalBus.OnSubmit = null;
            _button.onClick.Invoke();
        }
    }
}