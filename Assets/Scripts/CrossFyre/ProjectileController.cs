using CrossFyre.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace CrossFyre
{
    public class ProjectileController : MonoBehaviour
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

        private void Start()
        {
            ApplyStartingVelocity();
            collider.enabled = false;
            DOVirtual.DelayedCall(colliderActivationDelay, () => collider.enabled = true);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            IDamageable damageable = collision.collider.GetComponent<IDamageable>();
            if (damageable == null) return;
            damageable.TakeDamage(1);
            Destroy(gameObject);
        }

        private void ApplyStartingVelocity()
        {
            rigidbody2D.velocity += (Vector2)transform.right * velocity;
        }
    }
}
