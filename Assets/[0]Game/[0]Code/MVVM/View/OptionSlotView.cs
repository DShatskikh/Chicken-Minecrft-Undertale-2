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

        protected UIPanelOptionViewModel _viewModel;
        protected bool _isSelected;

        public void Init(UIPanelOptionViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.SelectionChanged += SetSelected;

            OnInit();
            
            SetSelected(viewModel.IsSelected);
            _slotName.text = _viewModel.Name;
        }

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            OnSetSelected(isSelected);
        }
        
        public abstract void OnSetSelected(bool isSelected);

        public abstract void OnInit();
    }
}