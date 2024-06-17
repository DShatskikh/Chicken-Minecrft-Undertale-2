using System;
using UnityEngine;

namespace Game
{
    public class UIPanelOptionViewModel
    {
        private string _name;
        private bool _isSelected;
        private Sprite _icon;

        public bool IsSelected { get => _isSelected; }
        public string Name { get => _name; }
        public Sprite Icon { get => _icon; }

        public event Action<bool> SelectionChanged;

        public UIPanelOptionViewModel(string name, bool isSelected, Sprite icon)
        {
            _name = name;
            _isSelected = isSelected;
            _icon = icon;
        }

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            SelectionChanged?.Invoke(isSelected);
        }
    }
}