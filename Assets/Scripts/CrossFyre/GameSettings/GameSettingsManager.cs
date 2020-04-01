using System;
using UnityEngine;

namespace CrossFyre.GameSettings
{
    public class GameSettingsManager : MonoBehaviour
    {
        public static event Action<Settings> SettingsChanged;

        private static GameSettingsManager _instance;

        private static Settings Settings
        {
            get => _instance.settingsObject.settings;
            set => _instance.settingsObject.settings = value;
        }

        [SerializeField] private GameSettingsObject settingsObject;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }

            _instance = this;
        }

        private void Start()
        {
            SettingsChanged?.Invoke(Settings);
        }


        public static void ChangeSettings(Settings settings)
        {
            Settings = settings;

            SettingsChanged?.Invoke(Settings);
        }

        public static void ChangeSettings(bool? lockJoystick = null)
        {
            var newSettings = new Settings
            {
                lockJoystick = lockJoystick ?? Settings.lockJoystick
            };

            ChangeSettings(newSettings);
        }
    }
}