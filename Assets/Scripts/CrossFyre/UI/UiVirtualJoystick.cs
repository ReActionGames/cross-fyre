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
            imgInnerThreshold = default,
            imgDeadThreshold = default,
            imgOriginPoint = default,
            imgTouchPoint = default;

        [Range(0f, 1f)] [SerializeField] private float activeAlpha = 0.8f, inactiveAlpha = 0.1f;

        private RectTransform rectFloatThreshold,
            rectInnerThreshold,
            rectDeadThreshold,
            rectOriginPoint,
            rectTouchPoint;

        private void Awake()
        {
            rectFloatThreshold = imgFloatThreshold.rectTransform;
            rectInnerThreshold = imgInnerThreshold.rectTransform;
            rectDeadThreshold = imgDeadThreshold.rectTransform;
            rectOriginPoint = imgOriginPoint.rectTransform;
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
            rectInnerThreshold.sizeDelta = new Vector2(data.innerThreshold * 2f, data.innerThreshold * 2f);
            rectDeadThreshold.sizeDelta = new Vector2(data.deadThreshold * 2f, data.deadThreshold * 2f);
        }

        private void UpdatePointPositions(InputData data)
        {
            rectOriginPoint.anchoredPosition = data.originPoint - (rectOriginPoint.sizeDelta / 2f);
            rectTouchPoint.anchoredPosition = data.touchPoint - (rectTouchPoint.sizeDelta / 2f);

            rectFloatThreshold.anchoredPosition = data.originPoint - (rectFloatThreshold.sizeDelta / 2f);
            rectInnerThreshold.anchoredPosition = data.originPoint - (rectInnerThreshold.sizeDelta / 2f);
            rectDeadThreshold.anchoredPosition = data.originPoint - (rectDeadThreshold.sizeDelta / 2f);
        }

        private void UpdateAlphas(InputData data)
        {
            SetAllInactive();
            switch (data.state)
            {
                case InputState.NotTouching:
                    break;
                case InputState.Dead:
                    imgDeadThreshold.color = imgDeadThreshold.color.With(a: activeAlpha);
                    // imgOriginPoint.color = imgOriginPoint.color.With(a: activeAlpha);
                    // imgTouchPoint.color = imgTouchPoint.color.With(a: activeAlpha);
                    break;
                case InputState.Inner:
                    imgInnerThreshold.color = imgInnerThreshold.color.With(a: activeAlpha);
                    // imgOriginPoint.color = imgOriginPoint.color.With(a: activeAlpha);
                    // imgTouchPoint.color = imgTouchPoint.color.With(a: activeAlpha);
                    break;
                case InputState.Normal:
                    imgFloatThreshold.color = imgFloatThreshold.color.With(a: activeAlpha);
                    // imgOriginPoint.color = imgOriginPoint.color.With(a: activeAlpha);
                    // imgTouchPoint.color = imgTouchPoint.color.With(a: activeAlpha);
                    break;
                case InputState.Sprint:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetAllInactive()
        {
            imgFloatThreshold.color = imgFloatThreshold.color.With(a: inactiveAlpha);
            imgInnerThreshold.color = imgInnerThreshold.color.With(a: inactiveAlpha);
            imgDeadThreshold.color = imgDeadThreshold.color.With(a: inactiveAlpha);
            // imgOriginPoint.color = imgOriginPoint.color.With(a: inactiveAlpha);
            // imgTouchPoint.color = imgTouchPoint.color.With(a: inactiveAlpha);
        }
    }
}