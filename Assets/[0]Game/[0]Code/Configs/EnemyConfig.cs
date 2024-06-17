using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public abstract class EnemyConfig : ScriptableObject
    {
        public GameObject[] Attacks;
        [FormerlySerializedAs("Attack")] public int Damage = 3;
        public Sprite View;
        [FormerlySerializedAs("Name")] public string DisplayName;
        [FormerlySerializedAs("WinPrize")] public int HealthPrize = 2;

        public abstract string GetReplica();
        public abstract List<(string, Action)> GetActs();
    }
}