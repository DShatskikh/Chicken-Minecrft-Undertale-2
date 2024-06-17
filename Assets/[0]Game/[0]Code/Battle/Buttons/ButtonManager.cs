using UnityEngine;

namespace Game
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField]
        private SwitchButton _currentButton;

        private void Update()
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                if (Input.GetAxisRaw("Horizontal") == 1 && _currentButton.RightButton)
                {
                    _currentButton.Unselect();
                    _currentButton = _currentButton.RightButton;
                    _currentButton.Select();
                }
                
                if (Input.GetAxisRaw("Horizontal") == -1 && _currentButton.LeftButton)
                {
                    _currentButton.Unselect();
                    _currentButton = _currentButton.LeftButton;
                    _currentButton.Select();
                }
            }
            
            if (Input.GetButtonDown("Submit"))
            {
                _currentButton.Click();
                gameObject.SetActive(false);
            }
        }

        public void Enable()
        {
            _currentButton.Select();
        }
    }
}