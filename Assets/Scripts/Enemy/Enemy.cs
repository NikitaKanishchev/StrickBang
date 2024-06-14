using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public float health = 2;
        private int expAmount = 100;
        
        private StateMachine stateMachine;
        private NavMeshAgent _agent;
        
        public NavMeshAgent Agent { get => _agent; }

        [SerializeField]
        private string currentState;

        public Path path;

        private GameObject player;
        public float sightDistance = 20f;
        public float fieldOfView = 85f;

        private void Start()
        {
            stateMachine = GetComponent<StateMachine>();
            _agent = GetComponent<NavMeshAgent>();
            stateMachine.Initialise();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public void TakeDamage(float damageAmount)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                damageAmount = 1f;
            }
            
            health -= damageAmount;
            
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            ExperienceManager.Instance.AddExperience(expAmount);
            Destroy(gameObject);
        }
    }
}
