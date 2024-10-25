using UnityEngine;

namespace drland.AlienSlayer
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorManager : MonoBehaviour
    {
        protected int _animIDSpeed;
        protected int _animIDMotionSpeed;
        protected int _animIDDeath;

        
        protected Animator _animator;

        public Animator Animator => _animator;
        
        public void Init()
        {
            _animator = GetComponent<Animator>();
            AssignAnimationIDs();
        }
        
        protected virtual void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDDeath = Animator.StringToHash("Death");
        }

        public void PlayWalkAnim(float value)
        {
            _animator.SetFloat(_animIDSpeed, value);
        }
        
        public void PlaySprintAnim(float value)
        {
            _animator.SetFloat(_animIDMotionSpeed, value);
        }
        
        public void PlayDeathAnim()
        {
            _animator.SetTrigger(_animIDDeath);
        }
    }
}