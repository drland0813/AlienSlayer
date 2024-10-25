using UnityEngine;

namespace drland.AlienSlayer
{
    public class PlayerAnimator : AnimatorManager
    {
        private int _animIDGrounded;
        
        public void PlayGroundAnim(bool value)
        {
            _animator.SetBool(_animIDGrounded, value);
        }
        
        protected override void AssignAnimationIDs()
        {
            base.AssignAnimationIDs();
            _animIDGrounded = Animator.StringToHash("Grounded");
        }

    }
}