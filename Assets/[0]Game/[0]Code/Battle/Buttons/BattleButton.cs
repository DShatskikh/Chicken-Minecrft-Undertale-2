using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class BattleButton : SwitchButton
    {
        [SerializeField]
        private UnityEvent _event;
        
        [SerializeField]
        private Image _view;

        [SerializeField]
        private Image _buttonFrame;
        
        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Color _selectColor;
        
        [SerializeField]
        private Color _unselectColor;
        
        private Sprite _icon;

        private void Start()
        {
            _icon = _view.sprite;
        }

        public override void Select()
        {
            _view.sprite = GameData.HeartSprite;
            _buttonFrame.color = _selectColor;
            _label.color = _selectColor;
        }

        public override void Unselect()
        {
            _view.sprite = _icon;
            _buttonFrame.color = _unselectColor;
            _label.color = _unselectColor;
        }

        public override void Click()
        {
            _event?.Invoke();
            Unselect();

            GameData.EffectAudioSource.clip = GameData.ClickSound;
            GameData.EffectAudioSource.Play();
        }
    }
}