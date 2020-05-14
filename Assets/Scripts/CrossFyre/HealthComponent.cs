using System;
using CrossFyre.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace CrossFyre
{
    public class HealthComponent : MonoBehaviour, IHealth, IDamageable
    {
        public int Health => _health;

        [SerializeField] private int _health = 100;
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _minHealth = 0;

        [Serializable]
        public class IntEvent : UnityEvent<int>
        {
        }

        [Serializable]
        public class GameObjectEvent : UnityEvent<GameObject>
        {
        }

        [Space]
        public IntEvent onHealthChanged = default;

        public UnityEvent onDeath = default;

        private void Start()
        {
            onHealthChanged?.Invoke(_health);
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;
            _health = Mathf.Clamp(_health, _minHealth, _maxHealth);

            onHealthChanged?.Invoke(_health);

            if (_health <= _minHealth)
            {
                onDeath?.Invoke();
            }
        }

        public void ResetHealth()
        {
            _health = _maxHealth;
            onHealthChanged?.Invoke(_health);
        }

        public void SetMaxHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
        }
    }
}
