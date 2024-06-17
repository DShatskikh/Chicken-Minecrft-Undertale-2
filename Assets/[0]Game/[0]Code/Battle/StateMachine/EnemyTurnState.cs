using System.Collections;
using UnityEngine;

namespace Game
{
    public class EnemyTurnState : BaseState
    {
        [SerializeField]
        private BattleDialogueBubble _dialogueBubble;

        public override void Enter()
        {
            GameData.Heart.gameObject.SetActive(true);
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
            yield return GameData.BattleStateMachine.Arena.AwaitToDefault();
            _dialogueBubble.Open(GameData.EnemyData.EnemyConfig.GetReplica());
            yield return new WaitUntil(() => !_dialogueBubble.IsTyping);
            yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
            
            GameData.BattleStateMachine.ChangeState(GameData.BattleStateMachine.PlayerTurnState);
        }
    }
}