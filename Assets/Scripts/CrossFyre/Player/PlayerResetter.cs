using System;
using CrossFyre.Interfaces;
using UnityEngine;

namespace CrossFyre.Player
{
    public class PlayerResetter : MonoBehaviour, IResetable
    {
        private PlayerController player;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
        }

        private void OnEnable()
        {
            GameEvents.GameEnded += DisablePlayer;
        }

        private void OnDisable()
        {
            GameEvents.GameEnded -= DisablePlayer;
        }

        private void EnablePlayer()
        {
            player.gameObject.SetActive(true);
            player.ResetState();
        }

        private void DisablePlayer()
        {
            player.gameObject.SetActive(false);
        }

        public void ResetObject()
        {
            EnablePlayer();
        }
    }
}