using System;
using System.Collections.Generic;
using CrossFyre.UI;
using UnityEngine;

namespace CrossFyre.GameInput
{
    public class PlayerInput : MonoBehaviour
    {
        public static event Action<InputData> InputChanged;

        private Dictionary<Type, IInputProvider> inputProviders;
        private IInputProvider currentInputProvider;

        private void Awake()
        {
            var controlPanels = FindObjectsOfType<UiControlPanel>();
            if (controlPanels.Length != 1)
            {
                Debug.LogError("There should be exactly one active UIControlPanel in the scene! Found: " +
                               controlPanels.Length);
            }

            var joystickInput = GetComponentInChildren<VirtualJoystickInput>();
            joystickInput.Init(controlPanels[0]);

            currentInputProvider = joystickInput;
        }

        private void Update()
        {
            InputChanged?.Invoke(currentInputProvider.GetInput());
        }

        public Vector3 GetInitialPosition()
        {
            return currentInputProvider.GetInitialPosition().With(z: 0);
        }
    }

    public enum InputState
    {
        NotTouching,
        Dead,
        Inner,
        Normal,
        Sprint
    }

    public struct InputData
    {
        public Vector2 input;

        public float floatThreshold;
        public float innerThreshold;
        public float deadThreshold;

        public InputState state;
        public Vector2 originPoint;
        public Vector2 touchPoint;
    }
}