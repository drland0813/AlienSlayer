using Drland.Cook;

namespace drland.AlienSlayer
{
    public class Player : Singleton<Player>
    {
        private PlayerEntity _playerEntity;
        private MovementManager _movementManager;
        private InputManager _input;
        private CameraManager _cameraManager;
        
        public AnimatorManager AnimatorManager => _playerEntity.AnimatorManager;
        public StatsManager StatsManager => _playerEntity.StatsManager;
        public InputManager Input => _input;
        public CameraManager CameraManager => _cameraManager;
        
        protected override void Awake()
        {
            base.Awake();
            _playerEntity = GetComponent<PlayerEntity>();
            _input = GetComponent<InputManager>();
            _cameraManager = GetComponent<CameraManager>();
            _movementManager = GetComponent<MovementManager>();
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
        }

        private void Update()
        {
            _movementManager.UpdateData();
        }
    }
}