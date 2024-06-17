using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public abstract class OptionSlotView : MonoBehaviour
    {
        [SerializeField]
        protected TextMeshProUGUI _slotName;

        [SerializeField] 
        private Image _selectIcon;

        [FormerlySerializedAs("_frame")] [SerializeField] 
        private Image _border;

        [SerializeField]
        protected Color _selectColor, _notSelectColor;
        
        protected UIPanelOptionViewModel _viewModel;

        private bool _isSelected;
        private Sprite _icon;

        public void Init(UIPanelOptionViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.SelectionChanged += SetSelected;

            _icon = viewModel.Icon;
            SetSelected(viewModel.IsSelected);
            _slotName.text = _viewModel.Name;

            OnInit();
        }

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            _selectIcon.sprite = isSelected ? GameData.HeartSprite : _icon;
            _border.color = isSelected ? _selectColor : _notSelectColor;
            _slotName.color = isSelected ? _selectColor : _notSelectColor;
        }

        public abstract void OnInit();
    }
}