using System.Collections;

namespace Game
{
    public class EnemyTurnState : BaseState
    {
        public override void Enter()
        {
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
        }
    }
}