using UnityEngine;

namespace drland.AlienSlayer
{
    public class EnemyAnimator : AnimatorManager
    {
        private int _animIDAttack;
        protected override void AssignAnimationIDs()
        {
            base.AssignAnimationIDs();
            _animIDAttack = Animator.StringToHash("Attack");
        }
        
        public void PlayAttackAnim()
        {
            _animator.SetTrigger(_animIDAttack);
        }

        public void EnableAttackLayer(bool enable)
        {
            var weight = enable ? 1 : 0;
            _animator.SetLayerWeight(1, weight);
        }

    }
}