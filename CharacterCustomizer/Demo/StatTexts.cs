using System.Collections.Generic;
using _Common.StatSystem;
using TMPro;
using UnityEngine;

namespace _Common.CharacterCustomizer.Demo {
    public sealed class StatTexts : MonoBehaviour {
        [SerializeField] private StatProvider statToDisplay;
        [SerializeField] private TextMeshProUGUI[] statTexts;

        private void OnEnable() {
            statToDisplay.OnStatChanged += OnStatChanged;
        }

        private void OnDisable() {
            statToDisplay.OnStatChanged -= OnStatChanged;
        }

        private void OnStatChanged(Dictionary<StatType, int> statDictionary) {
            var i = 0;
            foreach (var (statType, value) in statDictionary) {
                var statText = statTexts[i];
                statText.text = $"{statType}: {value}";
                i++;
            }
        }
    }
}