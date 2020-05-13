using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using CrossFyre.GameSettings;
using CrossFyre.Player;
using UnityEngine;
using UnityEngine.UI;

namespace CrossFyre.UI
{
    public class UiHealth : MonoBehaviour
    {
        [SerializeField] private Image heartPrefab;

        private List<Image> hearts = new List<Image>();

        private void Awake()
        {
            GetSpawnedHearts();
        }

        private void OnEnable()
        {
            GameSettingsManager.SettingsChanged += UpdateNumberOfHearts;
            GameEvents.PlayerHealthChanged += UpdateHealth;
        }

        private void OnDisable()
        {
            GameSettingsManager.SettingsChanged -= UpdateNumberOfHearts;
            GameEvents.PlayerHealthChanged -= UpdateHealth;
        }

        private void UpdateNumberOfHearts(Settings settings)
        {
            var maxNumHearts = (settings.playerHealth + 1) / 2;
            // var maxNumHearts = settings.playerHealth;

            if (hearts.Count == maxNumHearts) return;

            for (var i = 0; i < maxNumHearts; i = hearts.Count)
            {
                var heart = Instantiate(heartPrefab, transform);
                hearts.Add(heart);
            }
        }

        private void UpdateHealth(int health)
        {
            foreach (var heart in hearts)
            {
                heart.fillAmount = 0;
            }

            for (float i = 0; i < health / 2f; i += 0.5f)
            {
                hearts[(int) i].fillAmount += 0.5f;
            }

            // for (var i = 0; i < health; i++)
            // {
            //     hearts[i].fillAmount = 1;
            // }
            //
            // for (var i = health; i < hearts.Count; i++)
            // {
            //     hearts[i].fillAmount = 0;
            // }
        }

        private void GetSpawnedHearts()
        {
            var spawnedHearts = GetComponentsInChildren<Image>();
            foreach (var heart in spawnedHearts)
            {
                if (heart.sprite == heartPrefab.sprite)
                {
                    hearts.Add(heart);
                }
            }
        }
    }
}