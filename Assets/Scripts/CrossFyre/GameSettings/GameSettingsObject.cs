using Sirenix.OdinInspector;
using UnityEngine;

namespace CrossFyre.GameSettings
{
    public class GameSettingsObject : ScriptableObject
    {
        [HideLabel]
        public Settings settings;
    }
}