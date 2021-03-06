﻿using System;
using CrossFyre.Player;
using UnityEngine;

namespace CrossFyre.Gun
{
    [RequireComponent(typeof(HealthComponent))]
    public partial class GunController : MonoBehaviour
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
        private Flasher flasher;
    
        private void Awake()
        {
            health = GetComponent<HealthComponent>();
            player = FindObjectOfType<PlayerController>();
            flasher = new Flasher(flashDuration, flashColor, GetComponentsInChildren<SpriteRenderer>());
        }

        private void OnEnable()
        {
            health.OnDeath.AddListener(Die);
        }

        private void OnDisable()
        {
            health.OnDeath.RemoveListener(Die);
        }

        private void Die()
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }

        public void LookAtPlayer()
        {
            if (!player) return;

            transform.right = Vector3.Lerp(transform.right, (player.transform.position - transform.position), turnRate);
            //transform.right = player.transform.position - transform.position;
        }

        public void FireProjectile()
        {
            Instantiate(projectilePrefab, firePoint.position, transform.rotation);
        }

        public void StartFlash()
        {
            flasher.StartFlash();
        }

        public void StopFlash()
        {
            flasher.StopFlash();
        }
    }
}