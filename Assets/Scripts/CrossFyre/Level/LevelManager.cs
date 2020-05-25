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

        [SerializeField] private float delay = 1f;
        [SerializeField] private Node[] nodes;
    }

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Wave[] waves;

        private void Start()
        {
            if (waves.Length <= 0) return;
            
            StartCoroutine(SpawnWaveAsync(waves[0]));
        }

        private IEnumerator SpawnWaveAsync(Wave wave)
        {
            yield return new WaitForSeconds(wave.Delay);

            foreach (var node in wave.Nodes)
            {
                yield return new WaitForSeconds(node.delay);
                LeanPool.Spawn(node.gun, node.spawnPoint, Quaternion.identity);
            }
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