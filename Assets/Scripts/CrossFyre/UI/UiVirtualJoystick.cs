using System;
using CrossFyre.GameInput;
using ReActionGames.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFyre.UI
{
    public class UiVirtualJoystick : MonoBehaviour
    {
        [SerializeField] private Image imgFloatThreshold = default,
            imgTouchPoint = default;

        [Range(0f, 1f)] [SerializeField] private float activeAlpha = 0.8f, inactiveAlpha = 0.1f;

        private RectTransform rectFloatThreshold,
            rectTouchPoint;

        private void Awake()
        {
            rectFloatThreshold = imgFloatThreshold.rectTransform;
            rectTouchPoint = imgTouchPoint.rectTransform;
        }

        private void OnEnable()
        {
            PlayerInput.InputChanged += UpdateUi;
        }

        private void OnDisable()
        {
            PlayerInput.InputChanged -= UpdateUi;
        }

        private void UpdateUi(InputData data)
        {
            UpdateThresholdSizes(data);
            UpdatePointPositions(data);
            UpdateAlphas(data);
        }

        private void UpdateThresholdSizes(InputData data)
        {
            rectFloatThreshold.sizeDelta = new Vector2(data.floatThreshold * 2f, data.floatThreshold * 2f);
        }

        private void UpdatePointPositions(InputData data)
        {
            rectTouchPoint.anchoredPosition = data.touchPoint - (rectTouchPoint.sizeDelta / 2f);
            if (data.state == InputState.Sprint)
            {
                rectTouchPoint.anchoredPosition =
                    data.originPoint + ((data.touchPoint - data.originPoint).normalized * data.floatThreshold) -
                    (rectTouchPoint.sizeDelta / 2f);
            }

            rectFloatThreshold.anchoredPosition = data.originPoint - (rectFloatThreshold.sizeDelta / 2f);
        }

        private void UpdateAlphas(InputData data)
        {
            if (data.state == InputState.NotTouching)
            {
                SetAllInactive();
            }
            else
            {
                imgFloatThreshold.color = imgFloatThreshold.color.With(a: activeAlpha);
                imgTouchPoint.color = imgTouchPoint.color.With(a: activeAlpha);
            }
        }

        private void SetAllInactive()
        {
            imgFloatThreshold.color = imgFloatThreshold.color.With(a: inactiveAlpha);
            imgTouchPoint.color = imgTouchPoint.color.With(a: inactiveAlpha);
        }
    }
}