using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Common.CharacterCustomizer {
    public sealed class CharacterCustomizer : SerializedMonoBehaviour {
        [InfoBox("Skinned mesh renderers for mesh change / Holder transform for prefab change.")]
        [SerializeField] private readonly Dictionary<BodyParts, Transform> bodyPartTransforms = new();

        private readonly Dictionary<BodyParts, SkinnedMeshRenderer> _skinnedMeshRenderers = new();
        private readonly Dictionary<BodyParts, Mesh> _defaultMeshes = new();

        private readonly Dictionary<BodyParts, GameObject> _spawnedEquipments = new();

        private void Awake() {
            var targetList = bodyPartTransforms.Keys.ToList();
            targetList.Remove(BodyParts.HeadAttachment);
            targetList.Remove(BodyParts.EquipmentHolderLeft);
            targetList.Remove(BodyParts.EquipmentHolderRight);

            foreach (var bodyPart in targetList) {
                var targetMeshTransform = bodyPartTransforms[bodyPart];
                var skinnedRenderer = targetMeshTransform.GetComponent<SkinnedMeshRenderer>();
                _skinnedMeshRenderers.Add(bodyPart, skinnedRenderer);
                _defaultMeshes.Add(bodyPart, skinnedRenderer.sharedMesh);
            }
        }

        public void WearEquipment(EquipmentSO equipment) {
            var targetTransform = bodyPartTransforms[equipment.EquipPlace];

            if (equipment.UsePrefab()) SpawnPrefab(equipment, targetTransform);
            else ChangeMesh(equipment);
        }

        public void UnWearEquipment(EquipmentSO equipment) {
            if (equipment.UsePrefab()) {
                Destroy(_spawnedEquipments[equipment.EquipPlace]);
                _spawnedEquipments.Remove(equipment.EquipPlace);
            }
            else {
                ResetMesh(equipment.EquipPlace);
            }
        }

        private void ChangeMesh(EquipmentSO equipment) {
            var targetRenderer = _skinnedMeshRenderers[equipment.EquipPlace];
            targetRenderer.sharedMesh = equipment.Mesh;
        }

        private void ResetMesh(BodyParts bodyPart) {
            _skinnedMeshRenderers[bodyPart].sharedMesh = _defaultMeshes[bodyPart];
        }

        private void SpawnPrefab(EquipmentSO equipment, Transform targetTransform) {
            var spawnedGear = Instantiate(equipment.Prefab, targetTransform.position, targetTransform.rotation,
                targetTransform);
            _spawnedEquipments.Add(equipment.EquipPlace, spawnedGear);
        }
    }
}