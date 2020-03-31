using System;
using CrossFyre.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace CrossFyre.GameInput
{
    public class VirtualJoystickInput : MonoBehaviour, IInputProvider
    {
        // [SerializeField] private Vector2 sensitivity;
        [SerializeField] private float sprintThreshold = 150f;
        [SerializeField] private float normalThreshold = 140f;
        [SerializeField] private float innerThreshold = 115f;
        [SerializeField] private float deadThreshold = 40f;

        private bool isTouching;
        private bool lockJoystick;
        private Vector2 originPoint = Vector2.zero;
        private Vector2 touchPoint = Vector2.zero;


        private void OnEnable()
        {
            UiLockJoystick.LockJoystickChanged += OnLockJoystickChanged;
        }

        private void OnDisable()
        {
            UiLockJoystick.LockJoystickChanged -= OnLockJoystickChanged;
        }

        private void OnLockJoystickChanged(bool value)
        {
            lockJoystick = value;
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