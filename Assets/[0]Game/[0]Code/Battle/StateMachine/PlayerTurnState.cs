using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlayerTurnState : BaseState
    {
        [SerializeField]
        private MMF_Player _showFeedback;

        [SerializeField]
        private MMF_Player _hideFeedback;
        
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
            _hideFeedback.PlayFeedbacks();
            
            foreach (var button in GameData.BattleStateMachine.Buttons) 
                button.interactable = false;
        }

        private IEnumerator AwaitEnter()
        {
            yield return _showFeedback.PlayFeedbacksCoroutine(Vector3.zero);

            foreach (var button in GameData.BattleStateMachine.Buttons) 
                button.interactable = true;
        }
    }
}