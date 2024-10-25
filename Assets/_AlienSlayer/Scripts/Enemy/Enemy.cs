using System;
using System.Collections.Generic;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class Enemy : MonoBehaviour
    {
        public float Health;

        [SerializeField] private EnemyEntity _entity;
        private AIAgent _agent;
        private EnemyAttackComponent _attackComponent;
        private HealthComponent _healthComponent;
        private EnemyAnimator _animatorManager;
        private RagdollManager _ragdollManager;

        public HealthComponent HealthComponent => _healthComponent;
        public RagdollManager RagdollManager => _ragdollManager;
        public Action OnDeath;

        private void Awake()
        {
            _agent = GetComponent<AIAgent>();
            _attackComponent = GetComponent<EnemyAttackComponent>();
            _healthComponent = GetComponent<HealthComponent>();
            _ragdollManager = GetComponent<RagdollManager>();
        }

        private void Start()
        {
            Init();
        }
        
        private void Init()
        {
            _entity.Init();
            _agent.Init(_entity);
            _attackComponent.Init(_entity.StatsManager.Max.Damage);
            _healthComponent.Init(_entity.StatsManager);
            _ragdollManager.Init(_entity.AnimatorManager.Animator);

            _healthComponent.OnDeath += Death;

        }

        public void Death()
        {
            OnDeath?.Invoke();
            _ragdollManager.ActivateRagdoll();
            _agent.ChangeState(AIAgent.AIState.Death);
        }
        
        private void Update()
        {
            Health = _entity.StatsManager.Current.Health;
            _agent.UpdateData();
        }

        public void Stop()
        {
            _agent.Stop();
            _agent.ChangeState(AIAgent.AIState.Idle);
        }
    }
}
