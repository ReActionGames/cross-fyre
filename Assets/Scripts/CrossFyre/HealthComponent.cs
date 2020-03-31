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
        public IntEvent OnHealthChanged = default;

        public UnityEvent OnDeath = default;

        public void TakeDamage(int amount)
        {
            _health -= amount;
            _health = Mathf.Clamp(_health, _minHealth, _maxHealth);

            OnHealthChanged?.Invoke(amount);

            if (_health <= _minHealth)
            {
                OnDeath?.Invoke();
            }
        }
    }
}
