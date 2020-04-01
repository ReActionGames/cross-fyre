using System;
using Sirenix.OdinInspector;
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

        [InlineEditor(Expanded = true, DrawHeader = false), Space] [SerializeField]
        private GameSettingsObject settingsObject;

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

        // Called by Odin Button
        // ReSharper disable once UnusedMember.Local
        [Button(ButtonSizes.Gigantic, Name = "Update Settings"), PropertyOrder(-1)]
        private void NotifySettingsChanged()
        {
            SettingsChanged?.Invoke(Settings);
        }

        public static void ChangeSettings(object caller, Settings settings)
        {
            if (Settings.debugMode)
            {
                Debug.Log("Settings: Called by " + caller.GetType() + ";\nOld Settings: " + Settings + ";\nNew Settings: " + settings);
            }
            
            Settings = settings;

            SettingsChanged?.Invoke(Settings);
        }

        public static void ChangeSettings(object caller, bool? debugMode = null, bool? lockJoystick = null)
        {
            var newSettings = new Settings
            {
                debugMode = debugMode ?? Settings.debugMode,
                lockJoystick = lockJoystick ?? Settings.lockJoystick
            };

            ChangeSettings(caller, newSettings);
        }
    }
}