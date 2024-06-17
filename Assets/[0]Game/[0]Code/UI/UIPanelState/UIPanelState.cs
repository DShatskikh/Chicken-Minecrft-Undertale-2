using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class UIPanelState : MonoBehaviour
    {
        protected Dictionary<Vector2, UIPanelOptionViewModel> _viewModels = new Dictionary<Vector2, UIPanelOptionViewModel>();
        protected Vector2 _currentIndex;
        
        public abstract void HandleConfirmButtonClicked();
        public abstract void HandleBackButtonClicked();
        public abstract void HandleSelectedSlotChanged(Vector2 xy);
        public abstract UIPanelStateType GetUIPanelStateType();
        
        public virtual void Activate(bool active)
        {
            gameObject.SetActive(active);

            if (active)
            {
                Subscribe();
            }
            else
            {
                Unsubscribe();
            }
        }
        
        private void Subscribe()
        {
            GameData.InputController.OnBackButtonClicked += OnBackButtonClicked;
            GameData.InputController.OnConfirmButtonClicked += OnConfirmButtonClicked;
            GameData.InputController.OnSlotIndexChanged += OnSlotIndexChanged;
        }

        private void Unsubscribe()
        {
            GameData.InputController.OnBackButtonClicked -= OnBackButtonClicked;
            GameData.InputController.OnConfirmButtonClicked -= OnConfirmButtonClicked;
            GameData.InputController.OnSlotIndexChanged -= OnSlotIndexChanged;
        }
        
        private void OnConfirmButtonClicked()
        {
            if (gameObject.activeSelf)
            {
                HandleConfirmButtonClicked();
            }
        }

        private void OnBackButtonClicked()
        {
            if (gameObject.activeSelf)
            {
                HandleBackButtonClicked();
            }
        }

        private void OnSlotIndexChanged(int x, int y)
        {
            if (gameObject.activeSelf)
            {
                HandleSelectedSlotChanged(new Vector2(x, y));
            }
        }
    }
}