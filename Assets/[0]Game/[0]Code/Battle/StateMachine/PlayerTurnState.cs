using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlayerTurnState : BaseState
    {
        [SerializeField]
        private Button[] _buttons;

        [SerializeField]
        private MMF_Player _showFeedback;

        public override void Enter()
        {
            GameData.InputCanvas.SetActive(false);
            StartCoroutine(AwaitEnter()); 
        }

        public override void Upgrade()
        {
            
        }

        public override void Exit()
        {
            
        }

        private IEnumerator AwaitEnter()
        {
            yield return _showFeedback.PlayFeedbacksCoroutine(Vector3.zero);

            foreach (var button in _buttons) 
                button.interactable = true;
        }
    }
}