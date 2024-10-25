using Drland.Common.Utils;
using UnityEngine;

namespace drland.AlienSlayer
{
    public class Player : Singleton<Player>
    {
        private PlayerEntity _playerEntity;
        private MovementManager _movementManager;
        private InputManager _input;
        private CameraManager _cameraManager;
        private RigManager _rigManager;
        private HealthComponent _healthComponent;
        private RagdollManager _ragdollManager;
        public PlayerAnimator AnimatorManager => _playerEntity.AnimatorManager as PlayerAnimator;
        public StatsManager StatsManager => _playerEntity.StatsManager;
        public InputManager Input => _input;
        public CameraManager CameraManager => _cameraManager;

        public RigManager RigManager => _rigManager;

        public HealthComponent HealthComponent => _healthComponent;

        public CharacterUIInfo Info =>
            new()
            {
                Name = _playerEntity._entitySO.Name,
                Health = StatsManager.Max.Health
            };

        protected override void Awake()
        {
            Application.targetFrameRate = 60;
            base.Awake();
            _playerEntity = GetComponent<PlayerEntity>();
            _input = GetComponent<InputManager>();
            _cameraManager = GetComponent<CameraManager>();
            _movementManager = GetComponent<MovementManager>();
            _rigManager = GetComponent<RigManager>();
            _healthComponent = GetComponent<HealthComponent>();
            _ragdollManager = GetComponent<RagdollManager>();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _playerEntity.Init();
            _input.Init();
            _cameraManager.Init();
            _movementManager.Init();
            _rigManager.Init();
            _healthComponent.Init(_playerEntity.StatsManager);
            _ragdollManager.Init(_playerEntity.AnimatorManager.Animator);
            
            _healthComponent.OnDeath += Death;

        }

        private void Update()
        {
            if (_healthComponent.IsDead) return;
            
            _movementManager.UpdateData();
        }

        private void Death()
        {
            _ragdollManager.ActivateRagdoll();
        }
    }
}