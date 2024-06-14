using UnityEngine;

namespace Enemy.States
{
    public abstract class BaseState : MonoBehaviour
    {
        public Enemy enemy;
        public StateMachine stateMachine;
        public abstract void Enter();
        public abstract void Perform();
        public abstract void Exit();
    }
}
