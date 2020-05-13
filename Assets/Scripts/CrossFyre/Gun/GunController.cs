﻿using System;
using CrossFyre.Player;
using Lean.Pool;
using UnityEngine;

namespace CrossFyre.Gun
{
    [RequireComponent(typeof(HealthComponent), typeof(MonoFlash))]
    public partial class GunController : MonoBehaviour, IPoolable
    {
        public static event Action<GunController> OnDeath;

        [SerializeField] private float turnRate = 0.1f;
        [SerializeField] private Transform firePoint = null;
        [SerializeField] private Transform projectilePrefab = null;

        [Space]
        [SerializeField] private float flashDuration = 0.2f;
        [SerializeField] private Color flashColor = Color.white;

        private HealthComponent health;
        private PlayerController player;
        // private Flasher flasher;
        private MonoFlash flasher;
        
        private void Awake()
        {
            health = GetComponent<HealthComponent>();
            player = FindObjectOfType<PlayerController>();
            // flasher = new Flasher(flashDuration, flashColor, GetComponentsInChildren<SpriteRenderer>());
            flasher = GetComponent<MonoFlash>();
        }

        private void OnEnable()
        {
            health.onDeath.AddListener(Die);
        }

        private void OnDisable()
        {
            health.onDeath.RemoveListener(Die);
        }

        private void Die()
        {
            OnDeath?.Invoke(this);
            LeanPool.Despawn(gameObject);
        }

        public void LookAtPlayer()
        {
            if (!player) return;

            transform.right = Vector3.Lerp(transform.right, (player.transform.position - transform.position), turnRate);
        }

        public void FireProjectile()
        {
            LeanPool.Spawn(projectilePrefab, firePoint.position, transform.rotation);
        }

        public void StartFlash()
        {
            flasher.StartFlash();
        }

        public void StopFlash()
        {
            flasher.StopFlash();
        }

        public void OnSpawn()
        {
            health.ResetHealth();
        }

        public void OnDespawn()
        {
            flasher.StopFlash();
        }
    }
}