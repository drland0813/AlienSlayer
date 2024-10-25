using System;
using UnityEngine;
using UnityEngine.AI;

namespace drland.AlienSlayer
{
    public class AIAgent : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Transform _player;
        private EnemyAnimator _animatorManager;
        public void Init(AnimatorManager animatorManager)
        {
            _animatorManager = animatorManager as EnemyAnimator;
            _agent = GetComponent<NavMeshAgent>();
            _player = Player.Instance.transform;
        }

        public void UpdateData()
        {
            if (_player == null) return;

            _agent.destination = _player.position;
            _animatorManager.PlayWalkAnim(_agent.velocity.magnitude);
        }


    }
}