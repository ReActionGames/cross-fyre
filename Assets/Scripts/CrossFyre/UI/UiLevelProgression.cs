using System;
using CrossFyre.Core;
using CrossFyre.Level;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFyre.UI
{
    public class UiLevelProgression : MonoBehaviour
    {
        [SerializeField] private Image sliderForeground;
        [Range(0, 1)] [SerializeField] private float damping = 0.1f;
        [Range(0, 1)] [SerializeField] private float startingFillAmount = 0.05f;

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
            sliderForeground.fillAmount = startingFillAmount;
        }

        private void Update()
        {
            if (!levelStarted) return;

            var fillAmount = sliderForeground.fillAmount;
            fillAmount = Mathf.Lerp(fillAmount, 1 - levelManager.Progress, damping);
            fillAmount = Mathf.Clamp(fillAmount, startingFillAmount, 1);

            sliderForeground.fillAmount = fillAmount;
        }
    }
}