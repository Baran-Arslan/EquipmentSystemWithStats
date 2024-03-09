using System.Collections.Generic;
using _Common.StatSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Common.CharacterCustomizer {
    [CreateAssetMenu(fileName = "Equipment", menuName = "Game/Equipment")]
    public sealed class EquipmentSO : SerializedScriptableObject {
        [HideLabel, HorizontalGroup("Equipment"), LabelWidth(100)] [SerializeField]
        private BodyParts equipPlace;

        [AssetsOnly]
        [HorizontalGroup("Equipment"), LabelWidth(100)]
        [ShowIf("UsePrefab"), PreviewField(120)]
        [SerializeField]
        private GameObject prefab;

        [AssetsOnly]
        [HorizontalGroup("Equipment"), LabelWidth(100)]
        [HideIf("UsePrefab"), PreviewField(120)]
        [SerializeField]
        private Mesh mesh;

        [SerializeField] private StatModifier[] statModifiers;

        public GameObject Prefab => prefab;
        public BodyParts EquipPlace => equipPlace;
        public Mesh Mesh => mesh;
        public IEnumerable<StatModifier> StatModifiers => statModifiers;


        public bool UsePrefab() {
            return EquipPlace is BodyParts.EquipmentHolderLeft or BodyParts.EquipmentHolderRight
                or BodyParts.HeadAttachment;
        }
    }
}