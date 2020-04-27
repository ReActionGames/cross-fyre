using System;
using CrossFyre.Gun;
using CrossFyre.Interfaces;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace CrossFyre
{
    public class ProjectileController : MonoBehaviour,IPoolable
    {
        [SerializeField] private float colliderActivationDelay = 0.1f;
        [SerializeField] private float velocity = 5;

        private new Rigidbody2D rigidbody2D = null;
        private new Collider2D collider;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var damageable = collision.collider.GetComponent<IDamageable>();
            if (damageable == null) return;
            damageable.TakeDamage(1);
            LeanPool.Despawn(gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var gun = other.GetComponent<GunController>();
            if (gun == null) return;
            collider.isTrigger = false;
        }

        private void ApplyStartingVelocity()
        {
            rigidbody2D.velocity += (Vector2)transform.right * velocity;
        }

        public void OnSpawn()
        {
            ApplyStartingVelocity();
            collider.isTrigger = true;
        }

        public void OnDespawn()
        {
        }
    }
}
