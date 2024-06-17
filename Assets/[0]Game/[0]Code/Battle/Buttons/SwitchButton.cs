using UnityEngine;

namespace Game
{
    public abstract class SwitchButton : MonoBehaviour
    {
        public SwitchButton RightButton;
        public SwitchButton LeftButton;
        public SwitchButton UpButton;
        public SwitchButton DownButton;

        public abstract void Select();
        public abstract void Unselect();
        public abstract void Click();
    }
}