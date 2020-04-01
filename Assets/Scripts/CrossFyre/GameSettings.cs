using System;
using UnityEngine;

namespace CrossFyre
{
    public class GameSettings : MonoBehaviour
    {
        public static event Action<Settings> SettingsChanged;

        private static Settings Settings { get; set; }

        [SerializeField] private Settings defaultSettings;

        private void Awake()
        {
            Settings = defaultSettings;
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

    [Serializable]
    public struct Settings
    {
        public bool lockJoystick;
    }
}