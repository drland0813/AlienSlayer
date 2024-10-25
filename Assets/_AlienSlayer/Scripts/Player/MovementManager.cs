 using System;
 using drland.AlienSlayer;
 using UnityEngine;
 using UnityEngine.Serialization;
 using Random = UnityEngine.Random;
 

namespace drland.AlienSlayer
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementManager : MonoBehaviour
    {
        [Header("Player")]
        
        [SerializeField] private float _moveSpeed;

        [SerializeField] private float _sprintSpeed;

        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.24f;

        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;


        public bool Grounded = true;

        public float GroundedOffset = -0.14f;

        public float GroundedRadius = 0.32f;

        public LayerMask GroundLayers;

        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _targetShootRotation = 0.0f;

        private float _rotationVelocity;
        private float _verticalVelocity;


        private CharacterController _controller;
        private InputManager _input;
        private PlayerAnimator _animatorManager;
        private Camera _mainCamera;

        public void Init()
        {
            _controller = GetComponent<CharacterController>();
            _mainCamera = Camera.main;
            _input = Player.Instance.Input;
            _animatorManager = Player.Instance.AnimatorManager;

            _moveSpeed = Player.Instance.StatsManager.Max.Speed;
            _sprintSpeed = _moveSpeed * 2.5f;
        }

        public void UpdateData()
        {
            GroundedCheck();
            Move();
            RotateAndShoot();
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            _animatorManager.PlayGroundAnim(Grounded);
        }

        private void Move()
        {

            float targetSpeed = _input.Action.Sprint ? _sprintSpeed : _moveSpeed;

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.Action.Move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.Action.AnalogMovement ? _input.Action.Move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.Action.Move.x, 0.0f, _input.Action.Move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_input.Action.Move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;

            
                // // rotate to face input direction relative to camera position
                if (!_input.Action.Shoot)
                {
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }
            }
            
            
            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            Vector3 newPosition = targetDirection.normalized * (_speed * Time.deltaTime) +
                                  new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;

            // move the player
            _controller.Move(newPosition);

            _animatorManager.PlayWalkAnim(_animationBlend);
            _animatorManager.PlaySprintAnim(inputMagnitude);
        }

        private void RotateAndShoot()
        {
            Vector3 inputDirection = new Vector3(_input.Action.ShootDirection.x, 0.0f, _input.Action.ShootDirection.y).normalized;
            if (_input.Action.ShootDirection != Vector2.zero)
            {
                _targetShootRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                       _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetShootRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }
    }
}