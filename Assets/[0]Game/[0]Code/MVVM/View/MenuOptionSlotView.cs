using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MenuOptionSlotView : OptionSlotView
    {
        [SerializeField] 
        private Image _selectIcon;

        [SerializeField] 
        private Image _border;

        [SerializeField]
        private Color _selectColor, _notSelectColor;
        
        private Sprite _icon;

        public override void OnInit()
        {
            _icon = GameData.MenuIconStorage.GetIcon(((MenuOptionViewModel)_viewModel).OptionType);
        }

        public override void OnSetSelected(bool isSelected)
        {
            _selectIcon.sprite = isSelected ? GameData.HeartSprite : _icon;
            _border.color = isSelected ? _selectColor : _notSelectColor;
            _slotName.color = isSelected ? _selectColor : _notSelectColor;
        }
    }
}