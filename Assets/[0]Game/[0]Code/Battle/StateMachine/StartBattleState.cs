using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class StartBattleState : BaseState
    {
        [SerializeField]
        private SpriteRenderer _enemyView;

        [SerializeField]
        private AudioClip _startBattleClip;

        [SerializeField]
        private MMF_Player _feedback;

        [SerializeField]
        private AudioClip _music;
        
        public override void Enter()
        {
            _enemyView.sprite = GameData.EnemyData.EnemyConfig.View;

            var stateMachine = GameData.BattleStateMachine;
            stateMachine.PreviousSound = GameData.MusicAudioSource.clip;
            stateMachine.AttackIndex = 0;
            
            GameData.TimerBeforeAdsYG.gameObject.SetActive(false);

            var character = GameData.Character;
            character.enabled = false;
            character.GetComponent<Collider2D>().isTrigger = true;
            character.View.Flip(false);
            
            GameData.Health = GameData.MaxHealth;
            //EventBus.OnHealthChange.Invoke(GameData.MaxHealth, GameData.Health);
            
            GameData.BattleProgress = 0;
            SignalBus.OnBattleProgressChange?.Invoke(0);
            
            foreach (var button in stateMachine.Buttons) 
                button.interactable = false;
            
            stateMachine.CanvasGroup.alpha = 0;

            StartCoroutine(AwaitInitialization());
        }

        public override void Upgrade()
        {
            
        }

        public override void Exit()
        {
            
        }

        private IEnumerator AwaitInitialization()
        {
            GameData.EffectAudioSource.clip = _startBattleClip;
            GameData.EffectAudioSource.Play();

            yield return _feedback.PlayFeedbacksCoroutine(Vector3.zero);
            GameData.MusicAudioSource.clip = _music;
            GameData.MusicAudioSource.Play();
            GameData.BattleStateMachine.ChangeState(GameData.BattleStateMachine.PlayerTurnState);
        }
    }
}