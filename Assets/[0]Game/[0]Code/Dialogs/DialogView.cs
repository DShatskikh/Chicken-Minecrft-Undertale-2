using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DialogView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        
        [SerializeField]
        private TMP_Text _label;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetText(string text)
        {
            _label.text = text;
        }
    }
}