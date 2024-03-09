using System;
using System.Collections.Generic;
using _Common.iCare.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Common.StatSystem {
    public sealed class StatProvider : SerializedMonoBehaviour {
        [SerializeField] private StatSO baseStats;
        public event Action<Dictionary<StatType, int>> OnStatChanged;


        [ShowInInspector, ReadOnly]
        private Dictionary<StatType, int> _currentStats = new();
        private readonly Dictionary<StatModifier, float> _appliedModifiers = new();


        private void Start() {
            ResetOrInit();
        }


        private void ResetOrInit() {
            _currentStats = new Dictionary<StatType, int>(baseStats.BaseStats);
            OnStatChanged?.Invoke(_currentStats);
        }


        public void SetValue(StatType statType, int value) {
            _currentStats[statType] = value;
            OnStatChanged?.Invoke(_currentStats);
        }

        public void AddValue(StatType statType, int value) {
            _currentStats[statType] += value;
            OnStatChanged?.Invoke(_currentStats);
        }

        public void ApplyModifier(StatModifier statModifier) {
            var beforeValue = _currentStats[statModifier.StatType];
            
            var addAmount = statModifier.ModifierType switch {
                ModifierType.Flat => statModifier.Modifier,
                ModifierType.Percent => NumberExtensions.GetPercent(beforeValue, statModifier.Modifier),
                _ => throw new ArgumentOutOfRangeException()
            };

            var finalAddAmount = (int)addAmount;
            AddValue(statModifier.StatType, finalAddAmount);
            _appliedModifiers.Add(statModifier, finalAddAmount);
        }

        public void RemoveModifier(StatModifier statModifier) {
            if (_appliedModifiers.Remove(statModifier, out var value)) {
                AddValue(statModifier.StatType, -1 * (int)value);
            }
            else
                Debug.LogError("There is no modifier to remove!" + statModifier.ModifierType, gameObject);
        }

        public int GetValue(StatType statType) => _currentStats[statType];
    }
}