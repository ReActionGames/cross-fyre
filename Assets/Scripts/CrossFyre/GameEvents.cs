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

    public enum LevelEvent
    {
        LevelStarted,
        WaveStarted,
        WaveEnded,
        LevelEnded
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

        // LEVEL EVENTS //
        public static event Action LevelStarted;
        public static event Action WaveStarted;
        public static event Action WaveEnded;
        public static event Action LevelEnded;
        public static event Action<LevelEvent> OnLevelEvent;

        // PLAYER EVENTS //
        public static event Action<int> PlayerHealthChanged;
        public static event Action PlayerDied;
        public static event Action<PlayerEvent, PlayerState> OnPlayerEvent;


        // Trigger Methods //


        // public static void TriggerEvent(Enum gameEvent) 
        // {
        //     if (gameEvent is StandardEvent)
        //     {
        //         Debug.Log("standard event: ", gameEvent);
        //     }
        // }

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

        public static void TriggerLevelEvent(LevelEvent levelEvent)
        {
            switch (levelEvent)
            {
                case LevelEvent.LevelStarted:
                    LevelStarted?.Invoke();
                    break;
                case LevelEvent.WaveStarted:
                    WaveStarted?.Invoke();
                    break;
                case LevelEvent.WaveEnded:
                    WaveEnded?.Invoke();
                    break;
                case LevelEvent.LevelEnded:
                    LevelEnded?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(levelEvent), levelEvent, null);
            }

            OnLevelEvent?.Invoke(levelEvent);
        }
    }
}