using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace drland.AlienSlayer
{
    public class AIAgent : MonoBehaviour
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
        [SerializeField] private float _idleWaitTime = 5f; 
        [SerializeField] private float _patrolRadius = 20f;   
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _distanceRuntime;
        [SerializeField] private AIState _state;

        private AIState _currentState;
        private EnemyAnimator _animatorManager;
        private EnemyEntity _enemyEntity;
        private NavMeshAgent _agent;
        private Transform _player;

        public void Init(EnemyEntity enemyEntity)
        {
            _agent = GetComponent<NavMeshAgent>();
            _enemyEntity = enemyEntity;
            _player = Player.Instance.transform;
            _animatorManager = _enemyEntity.AnimatorManager as EnemyAnimator;
            _currentState = AIState.ChasePlayer;
            _walkSpeed = _enemyEntity.StatsManager.Max.Speed;
            _runSpeed = _walkSpeed * 2;
        }

        public void UpdateData()
        {
            if (ReferenceEquals(_player, null))
            {
                return;
            }

            _distanceRuntime = Vector3.Distance(transform.position, _player.position);
            _state = _currentState;
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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _animatorManager.PlayWalkAnim(_agent.velocity.magnitude);
        }

        private void Idle()
        {
            _agent.isStopped = true;
            _animatorManager.PlayWalkAnim(0);

            if (Vector3.Distance(transform.position, _player.position) < _chaseDistance)
            {
                ChangeState(AIState.ChasePlayer);
            }
        }

        private void ChasePlayer()
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.position);
            float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

            if (distanceToPlayer <= _attackDistance)
            {
                ChangeState(AIState.Attack);
            }
            // else if (distanceToPlayer > _chaseDistance)
            // {
            //     ChangeState(AIState.Idle);
            // }

            _agent.speed = distanceToPlayer > _attackDistance ? _runSpeed : _walkSpeed;
            _animatorManager.PlayWalkAnim(_agent.velocity.magnitude);
        }

        private void Attack()
        {
            if (!_animatorManager.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                _agent.isStopped = true;
                _animatorManager.EnableAttackLayer(true);
                _animatorManager.PlayAttackAnim();
            }
            if (_animatorManager.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                return;
            }

            if (!(Vector3.Distance(transform.position, _player.position) > _attackDistance)) return;
            
            _agent.isStopped = false;
            _animatorManager.EnableAttackLayer(false);
            ChangeState(AIState.ChasePlayer);
        }

        private void Death()
        {
            _agent.isStopped = true;
        }

        public void ChangeState(AIState newState)
        {
            _currentState = newState;
        }

        public void Stop()
        {
            _agent.Stop();
        }
    }

}