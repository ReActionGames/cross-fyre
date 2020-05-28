using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrossFyre.Gun;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrossFyre.Level
{
    public class LevelManager : MonoBehaviour
    {
        [InlineEditor(Expanded = true)] [SerializeField]
        private LevelData currentLevel;

        private int currentWave;
        private int gunsLeftInWave;
        private int gunsLeftInLevel;

        private List<GunController> spawnedGuns = new List<GunController>(15);

        private void OnEnable()
        {
            GunController.OnDeath += OnGunDeath;
        }

        private void OnDisable()
        {
            GunController.OnDeath -= OnGunDeath;
        }


        public void StartLevel(LevelData level)
        {
            currentLevel = level;

            GameEvents.TriggerLevelEvent(LevelEvent.LevelStarted);

            if (currentLevel.Waves.Length <= 0)
            {
                GameEvents.TriggerLevelEvent(LevelEvent.LevelEnded);
                return;
            }

            var totalGuns = 0;
            currentLevel.Waves.ForEach(wave => totalGuns += wave.TotalGuns);
            gunsLeftInLevel = totalGuns;

            currentWave = 0;
            StartCoroutine(SpawnWaveAsync(currentLevel.Waves[currentWave]));
        }

        private IEnumerator SpawnWaveAsync(Wave wave)
        {
            GameEvents.TriggerLevelEvent(LevelEvent.WaveStarted);

            spawnedGuns.Clear();

            gunsLeftInWave = wave.TotalGuns;
            yield return new WaitForSeconds(wave.Delay);

            foreach (var node in wave.Nodes)
            {
                yield return new WaitForSeconds(node.delay);
                var gun = LeanPool.Spawn(node.gun, node.spawnPoint, Quaternion.identity);
                spawnedGuns.Add(gun);
            }
        }

        private void OnGunDeath(GunController gun)
        {
            spawnedGuns.Remove(gun);

            gunsLeftInLevel--;
            gunsLeftInWave--;

            if (gunsLeftInWave == 1)
            {
                spawnedGuns[spawnedGuns.Count - 1].SelfDestruct();
                return;
            }

            if (gunsLeftInWave > 0) return;

            GameEvents.TriggerLevelEvent(LevelEvent.WaveEnded);
            StartNextWaveOrEndLevel();
        }

        private void StartNextWaveOrEndLevel()
        {
            currentWave++;

            if (currentWave < currentLevel.Waves.Length)
            {
                StartCoroutine(SpawnWaveAsync(currentLevel.Waves[currentWave]));
                return;
            }

            Debug.Log("End Level!!");
            GameEvents.TriggerLevelEvent(LevelEvent.LevelEnded);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (currentLevel == null) return;

            Gizmos.color = Color.green;
            foreach (var wave in currentLevel.Waves)
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