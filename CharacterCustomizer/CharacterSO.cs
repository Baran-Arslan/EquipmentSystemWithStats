using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Common.CharacterCustomizer {
    [CreateAssetMenu(fileName = "CharacterGear", menuName = "Game/Character")]
    public sealed class CharacterSO : SerializedScriptableObject {
        [SerializeField] private HashSet<EquipmentSO> equipmentData = new();
        public IEnumerable<EquipmentSO> EquipmentData => equipmentData;
    }
}