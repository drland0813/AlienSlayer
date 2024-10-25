using UnityEngine;
using UnityEngine.AI;

namespace drland.AlienSlayer
{
    public class AIStateMachine : MonoBehaviour
    {
        public enum AIState
        {
            Idle,
            ChasePlayer,
            Attack,
            Death
        }

        [SerializeField] private float _chaseDistance = 10f;
        [SerializeField] private float _attackDistance = 2f;
        [SerializeField] private float _walkSpeed = 1f;
        [SerializeField] private float _runSpeed = 3f;

        private AIState _currentState;
        private EnemyAnimator _animatorManager;
        private NavMeshAgent _agent;
        private Transform _player;

        void Init()
        {
            _animatorManager = GetComponent<EnemyAnimator>();
            _agent = GetComponent<NavMeshAgent>();
            _currentState = AIState.Idle;
        }

        void Update()
        {
            switch (_currentState)
            {
                case AIState.Idle:
                    Idle();
                    break;
                case AIState.ChasePlayer:
                    ChasePlayer();
                    break;
                case AIState.Attack:
                    Attack();
                    break;
                case AIState.Death:
                    Death();
                    break;
            }

            _animatorManager.PlayWalkAnim(_agent.velocity.magnitude);
        }

        void Idle()
        {
            _agent.isStopped = true;
            _animatorManager.PlayWalkAnim(0);

            if (Vector3.Distance(transform.position, _player.position) < _chaseDistance)
            {
                ChangeState(AIState.ChasePlayer);
            }
        }

        void ChasePlayer()
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.position);

            float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

            if (distanceToPlayer <= _attackDistance)
            {
                ChangeState(AIState.Attack);
            }
            else if (distanceToPlayer > _chaseDistance)
            {
                ChangeState(AIState.Idle);
            }

            // Nếu khoảng cách lớn, chạy nhanh hơn
            _agent.speed = distanceToPlayer > _attackDistance ? _runSpeed : _walkSpeed;
        }

        void Attack()
        {
            _agent.isStopped = true;
            _animatorManager.PlayAttackAnim();

            if (Vector3.Distance(transform.position, _player.position) > _attackDistance)
            {
                _agent.isStopped = false;
                ChangeState(AIState.ChasePlayer);
            }
        }

        private void Death()
        {
            _animatorManager.PlayDeathAnim();
            _agent.isStopped = true;
        }

        private void ChangeState(AIState newState)
        {
            _currentState = newState;
        }
    }

}