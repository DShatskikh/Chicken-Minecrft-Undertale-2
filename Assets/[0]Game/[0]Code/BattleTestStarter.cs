using System;
using UnityEngine;

namespace Game
{
    public class BattleTestStarter : MonoBehaviour
    {
        [SerializeField]
        private EnemyConfig _enemyConfig;

        private void Start()
        {
            GameData.EnemyData = new EnemyData()
            {
                EnemyConfig = _enemyConfig,
            };
            
            GetComponent<BattleStateMachine>().StartBattle();
        }
    }
}