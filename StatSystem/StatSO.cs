using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Common.StatSystem {
    [CreateAssetMenu(fileName = "Stat", menuName = "Game/Stats")]
    public sealed class StatSO : SerializedScriptableObject {
        [SerializeField] private readonly Dictionary<StatType, int> baseStats = new() {
            { StatType.Health, 100 },
            { StatType.Armor, 10 },
            { StatType.Damage, 10 },
            { StatType.AttackSpeed, 1 }
        };

        public Dictionary<StatType, int> BaseStats => baseStats;
    }
}