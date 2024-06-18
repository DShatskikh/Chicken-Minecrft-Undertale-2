using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DefaultOptionSlotView : OptionSlotView
    {
        [SerializeField] 
        private Image _selectIcon;
        
        public override void OnInit()
        {
            
        }

        public override void OnSetSelected(bool isSelected)
        {
            _selectIcon.gameObject.SetActive(isSelected);
        }
    }
}