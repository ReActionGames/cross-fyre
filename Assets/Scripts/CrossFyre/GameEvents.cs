using System;
using CrossFyre.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrossFyre
{
    public enum StandardEvent
    {
        GameStarted,
        GameEnded
    }

    public enum PlayerEvent
    {
        PlayerHealthChanged,
        PlayerDied
    }

    public static class GameEvents
    {
        // STANDARD EVENTS //
        public static event Action GameStarted;
        public static event Action GameEnded;
        public static event Action<StandardEvent> OnStandardEvent;
        
        // PLAYER EVENTS //
        public static event Action<int> PlayerHealthChanged;
        public static event Action PlayerDied;
        public static event Action<PlayerEvent, PlayerState> OnPlayerEvent;

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
        public static void TriggerPlayerEvent(PlayerEvent playerEvent, PlayerState state)
        {
            switch (playerEvent)
            {
                case PlayerEvent.PlayerHealthChanged:
                    PlayerHealthChanged?.Invoke(state.health);
                    break;
                case PlayerEvent.PlayerDied:
                    PlayerDied?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerEvent), playerEvent, null);
            }
            
            OnPlayerEvent?.Invoke(playerEvent, state);
        }
        
        
    }
}