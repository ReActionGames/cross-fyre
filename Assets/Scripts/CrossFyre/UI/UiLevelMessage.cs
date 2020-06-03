using System;
using CrossFyre.Core;
using CrossFyre.Level;
using DoozyUI;
using TMPro;
using UnityEngine;

namespace CrossFyre.UI
{
    public class UiLevelMessage : MonoBehaviour
    {
        // [SerializeField] private UIElement messageElement;
        [SerializeField] private string elementCategory = "MetaLevel", elementName = "Level Message";
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private string prefix = "Level ";
        [SerializeField] private string suffix = "";

        private void Start()
        {
            UIManager.HideUiElement(elementName, elementCategory, true);
            // messageElement.Hide(true);
            // messageElement.AutoHide(true, 0.1f);            
        }

        private void OnEnable()
        {
            GameEvents.LevelStarted += ShowLevelMessage;
        }

        private void OnDisable()
        {
            GameEvents.LevelStarted -= ShowLevelMessage;
        }

        private void ShowLevelMessage(LevelData data)
        {
            levelText.text = prefix + data.LevelNumber + suffix;
            // messageElement.Show(false);
            UIManager.ShowUiElement(elementName, elementCategory);
        }
    }
}
