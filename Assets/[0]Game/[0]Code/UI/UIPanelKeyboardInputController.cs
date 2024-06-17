using UnityEngine;

namespace Game
{
    public class UIPanelKeyboardInputController : UIPanelInputController
    {
        private void LateUpdate()
        {
            OnUpdate();
        }
        
        public override void OnUpdate()
        {
            if (Input.GetButtonDown("Submit"))
            {
                ConfirmButtonClicked();
                return;
            }

            if (Input.GetButtonDown("Cancel"))
            {
                BackButtonClicked();
                return;
            }

            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") == -1)
            {
                RowIndexChanged(1);
                return;
            }

            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") == 1)
            {
                RowIndexChanged(-1);
                return;
            }

            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") == 1)
            {
                ColumnIndexChanged(1);
                return;
            }

            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") == -1)
            {
                ColumnIndexChanged(-1);
                return;
            }
        }
    }
}