using UnityEngine;

namespace Game
{
    public class SubmitUpdater : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                SignalBus.OnSubmit?.Invoke();
            }
        }
    }
}