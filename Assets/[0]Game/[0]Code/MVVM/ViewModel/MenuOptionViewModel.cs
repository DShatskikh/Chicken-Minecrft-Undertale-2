using UnityEngine;

namespace Game
{
    public class MenuOptionViewModel : UIPanelOptionViewModel
    {
        private MenuOptionType _optionType;
        public MenuOptionType OptionType { get => _optionType; }

        public MenuOptionViewModel(string name, bool isSelected, MenuOptionType optionType, Sprite icon) : base(name, isSelected, icon)
        {
            _optionType = optionType;
        }
    }
}