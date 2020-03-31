using UnityEngine;

namespace CrossFyre.Gun
{
    [RequireComponent(typeof(GunController))]
    public class GunStateMachine : MonoBehaviour
    {
        private enum State
        {
            Patrol,
            Lock,
            Fire
        }

        [SerializeField] private float patrolDuration = 1.5f;
        [SerializeField] private float lockDuration = 0.5f;
        [SerializeField] private float fireDuration = 0.25f;

        private GunController controller;

        private State state;
        private float timeCounter = 0;

        private void Awake()
        {
            controller = GetComponent<GunController>();
        }

        private void Start()
        {
            state = State.Patrol;
        }

        private void Update()
        {
            timeCounter += Time.deltaTime;
            switch (state)
            {
                case State.Patrol:
                    Patrol();
                    break;

                case State.Lock:
                    Lock();
                    break;

                case State.Fire:
                    Fire();
                    break;
            }
        }

        private void TransitionTo(State newState)
        {
            timeCounter = 0;
            OnStateEntered(newState);
            state = newState;
        }

        private void OnStateEntered(State stateEntered)
        {
            switch (stateEntered)
            {
                case State.Patrol:
                
                    break;

                case State.Lock:
                    controller.StartFlash();
                    break;

                case State.Fire:
                    controller.FireProjectile();
                    break;
            }
        }

        //private void OnStateExited(State stateExited)
        //{
        //    switch (state)
        //    {
        //        case State.Patrol:

        //            break;

        //        case State.Lock:
        //            controller.StartFlash();
        //            break;

        //        case State.Fire:
        //            controller.FireProjectile();
        //            break;
        //    }
        //}

        private void Patrol()
        {
            controller.LookAtPlayer();

            if (timeCounter >= patrolDuration)
            {
                TransitionTo(State.Lock);
            }
        }

        private void Lock()
        {
            //controller.StartFlash();

            if (timeCounter >= lockDuration)
            {
                controller.StopFlash();
                TransitionTo(State.Fire);
            }
        }

        private void Fire()
        {
            //controller.FireProjectile();

            if (timeCounter >= fireDuration)
            {
                TransitionTo(State.Patrol);
            }
        }
    }
}