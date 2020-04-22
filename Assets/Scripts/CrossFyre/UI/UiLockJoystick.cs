using System;
using CrossFyre.GameSettings;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFyre.UI
{
    public class UiLockJoystick : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        private void Start()
        {
            if (!toggle)
            {
                Debug.LogError("No toggle set in inspector!");
                return;
            }
            
            toggle.onValueChanged.RemoveListener(OnToggled);
            toggle.onValueChanged.AddListener(OnToggled);
        }

        private void OnEnable()
        {
            GameSettingsManager.SettingsChanged += OnSettingsChanged;
        }

        private void OnDisable()
        {
            GameSettingsManager.SettingsChanged -= OnSettingsChanged;
        }

        private void OnSettingsChanged(Settings settings)
        {
            toggle.SetIsOnWithoutNotify(settings.lockJoystick);
        }

        public void OnToggled(bool value)
        {
            GameSettingsManager.ChangeSettings(this, lockJoystick: value);
        }
    }
}