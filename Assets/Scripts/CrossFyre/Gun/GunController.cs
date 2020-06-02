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

        private GunStateMachine stateMachine;
        private HealthComponent health;
        private PlayerController player;
        private MonoFlash flasher;

        private void Awake()
        {
            stateMachine = GetComponent<GunStateMachine>();
            health = GetComponent<HealthComponent>();
            player = FindObjectOfType<PlayerController>();
            flasher = GetComponent<MonoFlash>();
        }

        private void OnEnable()
        {
            health.onDeath.AddListener(Die);
            LookAtCenter();
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

        public void SelfDestruct()
        {
            Die();
        }

        private void LookAtCenter()
        {
            transform.right = Vector3.Lerp(transform.right, Vector3.zero - transform.position, 1f);
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
            stateMachine.ResetState();
            health.ResetHealth();
        }

        public void OnDespawn()
        {
            flasher.StopFlash();
        }
    }
}