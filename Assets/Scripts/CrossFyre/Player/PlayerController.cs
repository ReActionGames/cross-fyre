using CrossFyre.GameInput;
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
        }

        private void OnEnable()
        {
            Health.OnDeath.AddListener(Die);
            PlayerInput.InputChanged += ResolveInput;
        }


        private void OnDisable()
        {
            Health.OnDeath.RemoveListener(Die);
        }

        private void Start()
        {
            transform.position = inputProvider.GetInitialPosition();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void ResolveInput(InputData data)
        {
            var input = data.input;
            maxSpeed = data.state == InputState.Sprint ? fastSpeed : slowSpeed;
            Debug.Log(data.state);
            // maxSpeed = fastSpeed;
            // if (input.magnitude < 1f) maxSpeed = slowSpeed;
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