using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrossFyre
{
    public enum StandardEvent
    {
        GameStarted,
        GameEnded
    }

    public static class GameEvents
    {
        // STANDARD EVENTS //
        public static event Action GameStarted;
        public static event Action GameEnded;
        public static event Action<StandardEvent> OnStandardEvent;

        public static void TriggerStandardEvent(StandardEvent standardEvent)
        {
            switch (standardEvent)
            {
                case StandardEvent.GameStarted:
                    GameStarted?.Invoke();
                    break;
                case StandardEvent.GameEnded:
                    GameEnded?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(standardEvent), standardEvent, null);
            }
            
            OnStandardEvent?.Invoke(standardEvent);
        }
    }
}