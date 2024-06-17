using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Troll", menuName = "Data/Enemies/Troll", order = 30)]
    public class TrollEnemyConfig : EnemyConfig
    {
        public override string GetReplica()
        {
            if (GameData.CurrentPlayerTurn == PlayerTurn.Ban)
            {
                return "Не надо";
            }

            return "Это баг";
        }

        public override List<(string, Action)> GetActs()
        {
            return new List<(string, Action)>()
            {
                ("Пошутить", new Action(() => Debug.Log("wdww")))
            };
        }
    }
}