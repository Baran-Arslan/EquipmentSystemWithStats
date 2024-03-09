using System.Collections.Generic;
using _Common.StatSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Common.CharacterCustomizer {
    [RequireComponent(typeof(CharacterCustomizer))]
    [RequireComponent(typeof(StatProvider))]
    public sealed class EquipmentHandler : MonoBehaviour {
        private CharacterCustomizer _characterCustomizer;
        private StatProvider _statProvider;

        private readonly Dictionary<BodyParts, EquipmentSO> _equippedItems = new();

        private void Awake() {
            _characterCustomizer = GetComponent<CharacterCustomizer>();
            _statProvider = GetComponent<StatProvider>();
        }


        [Button]
        public void GearUp(CharacterSO characterData) {
            foreach (var equipment in characterData.EquipmentData) {
                Equip(equipment);
            }
        }

        [Button]
        public void Equip(EquipmentSO equipment) {
            if (_equippedItems.TryGetValue(equipment.EquipPlace, out var item)) {
                UnEquip(item);
            }

            _characterCustomizer.WearEquipment(equipment);

            var statModifiers = equipment.StatModifiers;
            foreach (var statModifier in statModifiers) {
                _statProvider.ApplyModifier(statModifier);
            }

            _equippedItems.Add(equipment.EquipPlace, equipment);
        }

        [Button]
        public void UnEquip(EquipmentSO equipment) {
            if (!_equippedItems.ContainsValue(equipment)) {
                Debug.LogError("Equipment is not equipped." + equipment.name, gameObject);
                return;
            }
            
            _characterCustomizer.UnWearEquipment(equipment);
            _equippedItems.Remove(equipment.EquipPlace);
            foreach (var modifier in equipment.StatModifiers) {
                _statProvider.RemoveModifier(modifier);
            }
        }
    }
}