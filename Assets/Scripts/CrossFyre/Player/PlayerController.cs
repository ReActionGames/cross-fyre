using System;
using CrossFyre.GameInput;
using CrossFyre.GameSettings;
using UnityEngine;

namespace CrossFyre.Player
{
    [Serializable]
    public struct PlayerState
    {
        public int health;
        public bool dead;
    }

    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerState State => state;

        [SerializeField] private float fastSpeed = 6f;
        [SerializeField] private float slowSpeed = 4f;
        [SerializeField] private float acceleration = 20f;

        private PlayerInput inputProvider;
        private new Rigidbody2D rigidbody2D;
        private HealthComponent health;

        private PlayerState state;
        private Vector2 inputCache = Vector2.zero;
        private float maxSpeed = 6f;

        private void Awake()
        {
            health = GetComponent<HealthComponent>();
            inputProvider = GetComponent<PlayerInput>();
            rigidbody2D = GetComponent<Rigidbody2D>();

            health.SetMaxHealth(GameSettingsManager.Settings.playerHealth);
            health.ResetHealth();

            state = new PlayerState
            {
                health = health.Health,
                dead = false
            };
        }

        private void OnEnable()
        {
            health.onDeath.AddListener(Die);
            health.onHealthChanged.AddListener(OnHealthChanged);
            PlayerInput.InputChanged += ResolveInput;
        }

        private void OnDisable()
        {
            health.onDeath.RemoveListener(Die);
            health.onHealthChanged.RemoveListener(OnHealthChanged);
            PlayerInput.InputChanged -= ResolveInput;
        }

        private void OnHealthChanged(int newHealth)
        {
            state.health = newHealth;
            GameEvents.TriggerPlayerEvent(PlayerEvent.PlayerHealthChanged, state);
        }

        private void Start()
        {
            transform.position = inputProvider.GetInitialPosition();
        }

        public void ResetState()
        {
            transform.position = inputProvider.GetInitialPosition();
            state.dead = false;
            health.ResetHealth();
        }

        private void Die()
        {
            state.dead = true;
            GameEvents.TriggerPlayerEvent(PlayerEvent.PlayerDied, state);
            GameEvents.TriggerStandardEvent(StandardEvent.GameEnded);
            gameObject.SetActive(false);
        }

        private void ResolveInput(InputData data)
        {
            var input = data.input;
            maxSpeed = data.state == InputState.Sprint ? fastSpeed : slowSpeed;
            inputCache += input;
        }

        private void FixedUpdate()
        {
            rigidbody2D.AddForce(inputCache * acceleration);
            if (rigidbody2D.velocity.magnitude > maxSpeed)
            {
                rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed;
            }

            inputCache = Vector2.zero;
        }
    }
}