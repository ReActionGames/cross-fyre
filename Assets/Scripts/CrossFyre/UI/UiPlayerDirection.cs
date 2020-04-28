using System;
using CrossFyre.GameInput;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFyre.UI
{
    public class UiPlayerDirection : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Color sprintColor = Color.yellow;

        private RectTransform rect; 
        private Color mainColor;
        private float lastAngle = 0f;

        private void Awake()
        {
            rect = image.rectTransform;
            mainColor = image.color;
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
            if (data.state == InputState.NotTouching)
            {
                image.enabled = false;
                return;
            }

            image.enabled = true;

            image.color = data.state == InputState.Sprint ? sprintColor : mainColor;
            
            var angle = data.Direction;
            if (Mathf.Approximately(angle, 0f))
            {
                angle = lastAngle;
            }
            
            rect.rotation = Quaternion.Euler(0, 0, angle);
            lastAngle = angle;
        }
    }
}