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

        [SerializeField]
        private ButtonManager _buttonManager;

        [SerializeField]
        private TypingText _typingText;
        
        public override void Enter()
        {
            GameData.InputCanvas.SetActive(false);
            _typingText.Write($"*{GameData.EnemyData.EnemyConfig.DisplayName} преграждает вам путь");
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
            
            _typingText.Write("");
        }

        private IEnumerator AwaitEnter()
        {
            yield return _showFeedback.PlayFeedbacksCoroutine(Vector3.zero);

            foreach (var button in GameData.BattleStateMachine.Buttons) 
                button.interactable = true;
            
            _buttonManager.Enable();
        }
    }
}