using System;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFyre.UI
{
    public class UiLockJoystick : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        private void OnEnable()
        {
            GameSettings.SettingsChanged += OnSettingsChanged;
        }

        private void OnDisable()
        {
            GameSettings.SettingsChanged -= OnSettingsChanged;
        }

        private void OnSettingsChanged(Settings settings)
        {
            toggle.SetIsOnWithoutNotify(settings.lockJoystick);
        }

        public void OnToggled(bool value)
        {
            GameSettings.ChangeSettings(lockJoystick: toggle);
        }
    }
}