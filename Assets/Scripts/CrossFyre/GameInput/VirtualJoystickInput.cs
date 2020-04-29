using System;
using CrossFyre.GameSettings;
using CrossFyre.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CrossFyre.GameInput
{
    public class VirtualJoystickInput : MonoBehaviour, IInputProvider
    {
        [SerializeField] private float originalSprintThreshold = 150f;
        [SerializeField] private float originalNormalThreshold = 140f;
        [SerializeField] private float originalInnerThreshold = 115f;
        [SerializeField] private float originalDeadThreshold = 20f;
        [SerializeField] private float factor = 567.53f;

        [ShowInInspector, ReadOnly]
        private float sprintThreshold,
            normalThreshold,
            innerThreshold,
            deadThreshold;

        private bool isTouching;
        private bool lockJoystick;
        private Vector2 originPoint = Vector2.zero;
        private Vector2 touchPoint = Vector2.zero;

        private void Start()
        {
            RecalculateThresholds();
        }

        [Button]
        private void RecalculateThresholds()
        {
            var dpi = Screen.dpi / factor;
            // Debug.Log(dpi);

            sprintThreshold = originalSprintThreshold / dpi;
            normalThreshold = originalNormalThreshold / dpi;
            innerThreshold = originalInnerThreshold / dpi;
            deadThreshold = originalDeadThreshold / dpi;
        }

        private void OnEnable()
        {
            GameSettingsManager.SettingsChanged += OnLockJoystickChanged;
        }

        private void OnDisable()
        {
            GameSettingsManager.SettingsChanged -= OnLockJoystickChanged;
        }

        private void OnLockJoystickChanged(Settings settings)
        {
            lockJoystick = settings.lockJoystick;
        }

        public void Init(UiControlPanel controlPanel)
        {
            controlPanel.BeginDrag += BeginDrag;
            controlPanel.Drag += Drag;
            controlPanel.EndDrag += EndDrag;
        }

        private void BeginDrag(PointerEventData data)
        {
            isTouching = true;
            originPoint = data.position;
            touchPoint = data.position;
        }

        private void Drag(PointerEventData data)
        {
            touchPoint = data.position;
            if (!lockJoystick)
            {
                originPoint = CalcOriginPoint(originPoint, touchPoint, sprintThreshold);
            }
        }

        public static Vector2 CalcOriginPoint(Vector2 originPoint, Vector2 touchPoint, float threshold)
        {
            var distance = Vector2.Distance(originPoint, touchPoint);

            if (distance <= threshold) return originPoint;


            var percent = 1 - (threshold / distance);
            return Vector2.Lerp(originPoint, touchPoint, percent);
        }

        private void EndDrag(PointerEventData data)
        {
            isTouching = false;
        }

        public Vector3 GetInitialPosition()
        {
            return Vector2.zero;
        }

        public InputData GetInput()
        {
            var data = new InputData
            {
                deadThreshold = deadThreshold,
                floatThreshold = sprintThreshold,
                innerThreshold = innerThreshold,
                originPoint = originPoint,
                touchPoint = touchPoint
            };

            if (!isTouching)
            {
                data.state = InputState.NotTouching;
                data.input = Vector2.zero;
                return data;
            }

            var direction = touchPoint - originPoint;

            if (direction.magnitude <= deadThreshold)
            {
                data.state = InputState.Dead;
                data.input = Vector2.zero;
                return data;
            }

            if (direction.magnitude <= innerThreshold)
            {
                var input = direction.normalized * CalcPercentageToThreshold(direction, innerThreshold);

                data.state = InputState.Inner;
                data.input = input;
                return data;
            }

            data.state = direction.magnitude <= normalThreshold ? InputState.Normal : InputState.Sprint;
            data.input = direction.normalized;
            return data;
        }

        private static float CalcPercentageToThreshold(Vector2 direction, float threshold)
        {
            return direction.magnitude / threshold;
        }
    }
}