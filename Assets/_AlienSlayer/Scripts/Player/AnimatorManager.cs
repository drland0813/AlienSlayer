using UnityEngine;

namespace drland.AlienSlayer
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorManager : MonoBehaviour
    {
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDMotionSpeed;
        
        private Animator _animator;
        
        public void Init()
        {
            _animator = GetComponent<Animator>();
            AssignAnimationIDs();
        }
        
        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public void PlayGroundAnim(bool value)
        {
            _animator.SetBool(_animIDGrounded, value);
        }
        
        public void PlayWalkAnim(float value)
        {
            _animator.SetFloat(_animIDSpeed, value);
        }
        
        public void PlaySprintAnim(float value)
        {
            _animator.SetFloat(_animIDMotionSpeed, value);
        }
    }
}