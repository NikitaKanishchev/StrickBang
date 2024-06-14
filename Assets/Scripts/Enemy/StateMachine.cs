using Enemy.States;
using UnityEngine;

namespace Enemy
{
    public class StateMachine : MonoBehaviour
    {
        private BaseState activeState;
        private PatrolState patrolState;
        public void Initialise()
        {
            patrolState = gameObject.AddComponent<PatrolState>();
            ChangeState(patrolState);
        }
        private void Update()
        {
            if (activeState != null)
            {
                activeState.Perform();
            }
        }

        public void ChangeState(BaseState newState)
        {
            if (activeState != null)
            {
                activeState.Exit();
            }

            activeState = newState;

            if (activeState != null)
            {
                activeState.stateMachine = this;
                activeState.enemy = GetComponent<Enemy>();
                activeState.Enter();
            }
        }
    }
}