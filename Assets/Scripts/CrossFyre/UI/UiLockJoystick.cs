using System;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFyre.UI
{
    public class UiLockJoystick : MonoBehaviour
    {
        public static event Action<bool> LockJoystickChanged;

        [SerializeField] private Toggle toggle;

        private void Start()
        {
            LockJoystickChanged?.Invoke(toggle.isOn);
        }

        public void OnToggled(bool value)
        {
            LockJoystickChanged?.Invoke(value);
        }
    }
}
