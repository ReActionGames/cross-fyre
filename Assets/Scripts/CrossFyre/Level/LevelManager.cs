using System;
using System.Collections;
using System.Collections.Generic;
using CrossFyre.Gun;
using Lean.Pool;
using UnityEditor;
using UnityEngine;

namespace CrossFyre.Level
{
    [Serializable]
    public struct Node
    {
        public float delay;
        public GunController gun;
        public Vector3 spawnPoint;
    }

    [Serializable]
    public class Wave
    {
        public Node[] Nodes => nodes;
        public float Delay => delay;
        public int totalGuns => nodes.Length;

        [SerializeField] private float delay = 1f;
        [SerializeField] private Node[] nodes;
    }

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Wave[] waves;

        private int currentWave;
        private int gunsLeftInWave;
        private int gunsLeftInLevel;

        private void OnEnable()
        {
            GunController.OnDeath += OnGunDeath;
        }

        private void OnDisable()
        {
            GunController.OnDeath -= OnGunDeath;
        }

        private void Start()
        {
            StartLevel();
        }

        private void StartLevel()
        {
            GameEvents.TriggerLevelEvent(LevelEvent.LevelStarted);

            if (waves.Length <= 0)
            {
                GameEvents.TriggerLevelEvent(LevelEvent.LevelEnded);
                return;
            }

            var totalGuns = 0;
            waves.ForEach(wave => totalGuns += wave.totalGuns);
            gunsLeftInLevel = totalGuns;

            currentWave = 0;
            StartCoroutine(SpawnWaveAsync(waves[currentWave]));
        }

        private IEnumerator SpawnWaveAsync(Wave wave)
        {
            GameEvents.TriggerLevelEvent(LevelEvent.WaveStarted);

            gunsLeftInWave = wave.totalGuns;
            yield return new WaitForSeconds(wave.Delay);

            foreach (var node in wave.Nodes)
            {
                yield return new WaitForSeconds(node.delay);
                LeanPool.Spawn(node.gun, node.spawnPoint, Quaternion.identity);
            }
        }

        private void OnGunDeath(GunController gun)
        {
            gunsLeftInLevel--;
            gunsLeftInWave--;

            if (gunsLeftInWave > 0) return;

            GameEvents.TriggerLevelEvent(LevelEvent.WaveEnded);
            StartNextWaveOrEndLevel();
        }

        private void StartNextWaveOrEndLevel()
        {
            currentWave++;

            if (currentWave < waves.Length)
            {
                StartCoroutine(SpawnWaveAsync(waves[currentWave]));
                return;
            }

            Debug.Log("End Level!!");
            GameEvents.TriggerLevelEvent(LevelEvent.LevelEnded);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (var wave in waves)
            {
                foreach (var node in wave.Nodes)
                {
                    Gizmos.DrawWireSphere(node.spawnPoint, 0.25f);
                }
            }
        }
#endif
    }
}