using CrossFyre.GameInput;
using CrossFyre.GameSettings;
using UnityEngine;

namespace CrossFyre.Player
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        public HealthComponent Health { get; private set; }

        [SerializeField] private float fastSpeed = 6f;
        [SerializeField] private float slowSpeed = 4f;
        [SerializeField] private float acceleration = 20f;

        private PlayerInput inputProvider;
        private new Rigidbody2D rigidbody2D;

        private Vector2 inputCache = Vector2.zero;
        private float maxSpeed = 6f;

        private void Awake()
        {
            Health = GetComponent<HealthComponent>();
            inputProvider = GetComponent<PlayerInput>();
            rigidbody2D = GetComponent<Rigidbody2D>();

            Health.SetMaxHealth(GameSettingsManager.Settings.playerHealth);
            Health.ResetHealth();
        }

        private void OnEnable()
        {
            Health.onDeath.AddListener(Die);
            Health.onDeath.AddListener(TriggerDieEvent);
            Health.onHealthChanged.AddListener(TriggerOnHealthChangedEvent);
            PlayerInput.InputChanged += ResolveInput;
        }

        private void OnDisable()
        {
            Health.onDeath.RemoveListener(Die);
            Health.onDeath.RemoveListener(TriggerDieEvent);
            Health.onHealthChanged.RemoveListener(TriggerOnHealthChangedEvent);
        }

        private static void TriggerOnHealthChangedEvent(int health)
        {
            GameEvents.TriggerPlayerEvent(PlayerEvent.PlayerHealthChanged, health);
        }

        private static void TriggerDieEvent()
        {
            GameEvents.TriggerPlayerEvent(PlayerEvent.PlayerDied);
        }

        private void Start()
        {
            transform.position = inputProvider.GetInitialPosition();
        }

        private void Die()
        {
            GameEvents.TriggerStandardEvent(StandardEvent.GameEnded);
            Destroy(gameObject);
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