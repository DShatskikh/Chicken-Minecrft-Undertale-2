using UnityEngine;

namespace Game
{
    public class StartBattleState : BaseState
    {
        public override void Enter()
        {
            var stateMachine = GameData.BattleStateMachine;
            stateMachine.PreviousSound = GameData.MusicAudioSource.clip;
            stateMachine.AttackIndex = 0;
            
            GameData.TimerBeforeAdsYG.gameObject.SetActive(false);
            //GameData.Battle.transform.position = Camera.main.transform.position.SetZ(0).AddY(-3.5f) +
            //                                     (Vector3) GameData.EnemyData.StartBattleTrigger.Offset;
            
            GameData.EnemyData.GameObject.transform.SetParent(GameData.EnemyPoint);

            if (GameData.EnemyData.GameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.flipX = true;
            }
            
            var character = GameData.Character;
            character.enabled = false;
            character.GetComponent<Collider2D>().isTrigger = true;
            character.View.Flip(false);
            
            GameData.Health = GameData.MaxHealth;
            EventBus.OnHealthChange.Invoke(GameData.MaxHealth, GameData.Health);
            
            GameData.BattleProgress = 0;
            EventBus.OnBattleProgressChange?.Invoke(0);
            
            foreach (var button in stateMachine.Buttons) 
                button.interactable = false;
            
            stateMachine.CanvasGroup.alpha = 0;
            stateMachine.ChangeState(stateMachine.PlayerTurnState);
        }

        public override void Upgrade()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}