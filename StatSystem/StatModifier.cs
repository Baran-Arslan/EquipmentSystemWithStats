using UnityEngine;

namespace _Common.StatSystem {
    public enum ModifierType {
        Flat,
        Percent
    }
    [System.Serializable]
    public class StatModifier {
        [SerializeField] private StatType statType;
        [SerializeField] private ModifierType modifierType;
        [SerializeField] private float modifier = 1f;

        public StatType StatType => statType;
        public ModifierType ModifierType => modifierType;
        public float Modifier => modifier;
    }
}