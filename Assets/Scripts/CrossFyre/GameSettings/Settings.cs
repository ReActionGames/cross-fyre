using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrossFyre.GameSettings
{
    [Serializable]
    public struct Settings
    {
        [TitleGroup("Debug")]
        public bool debugMode;
        
        [TitleGroup("User Settings")]
        public bool lockJoystick;

        public override string ToString()
        {
            return JsonUtility.ToJson(this, true);
        }
    }
}