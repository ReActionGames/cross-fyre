﻿using Lean.Pool;
using UnityEngine;

namespace CrossFyre
{
    public class LeanPoolSelfDestruct : MonoBehaviour, IPoolable
    {
        [SerializeField] private float delay = 1f;

        public void OnSpawn()
        {
            LeanPool.Despawn(gameObject, delay);
        }

        public void OnDespawn()
        {
        }
    }
}