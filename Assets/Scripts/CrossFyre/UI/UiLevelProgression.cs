using System;
using CrossFyre.Core;
using CrossFyre.Level;
using Doozy.Engine.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFyre.UI
{
    public class UiLevelProgression : MonoBehaviour
    {
        // [SerializeField] private Image sliderForeground;
        [SerializeField] private Progressor progressor;
        // [Range(0, 1)] [SerializeField] private float damping = 0.1f;
        [Range(0, 1)] [SerializeField] private float startingValue = 0.05f;

        private LevelManager levelManager;

        private bool levelStarted = false;

        private void OnEnable()
        {
            GameEvents.LevelStarted += StartTrackingProgress;
        }

        private void OnDisable()
        {
            GameEvents.LevelStarted -= StartTrackingProgress;
            
        }

        private void StartTrackingProgress(LevelData data)
        {
            levelManager = FindObjectOfType<LevelManager>();
            levelStarted = true;
        }

        private void Start()
        {
            // sliderForeground.fillAmount = startingValue;
            progressor.SetValue(startingValue);
        }

        private void Update()
        {
            if (!levelStarted) return;

            var value = levelManager.Progress;
            value = Mathf.Clamp(value, startingValue, 1);
            
            progressor.SetValue(value);
            //
            // var fillAmount = sliderForeground.fillAmount;
            // fillAmount = Mathf.Lerp(fillAmount, 1 - levelManager.Progress, damping);
            // fillAmount = Mathf.Clamp(fillAmount, startingValue, 1);
            //
            // sliderForeground.fillAmount = fillAmount;
        }
    }
}