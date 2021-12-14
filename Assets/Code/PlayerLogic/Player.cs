using Code.PlayerLogic.ControllersData;
using Code.PlayerLogic.PlayerSettings;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Code.PlayerLogic
{
    [RequireComponent(typeof(PlayerMoveController))]
    [RequireComponent(typeof(PlayerShootController))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private InputCatcher inputCatcher;
        [SerializeField] private Transform fpsCameraPosition;
        [SerializeField] private Transform topDawnCameraPositon;
        [SerializeField] private Transform spawnBulletPlace;
        [SerializeField] private PlayerSettingsData settings;
        [SerializeField] private Button moveStateOnButton;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform model;
        
        private PlayerMoveController _playerMoveController;
        private PlayerShootController _playerShootController;
        private CameraController _camera;
        private NavMeshAgent _agent;
        
        private void Awake()
        {
            _camera = Camera.main.GetComponent<CameraController>();
            _camera.Init(settings.Speed,transform,settings.CameraOffset);
            _playerMoveController = GetComponent<PlayerMoveController>();
            _playerShootController = GetComponent<PlayerShootController>();

            _agent = GetComponent<NavMeshAgent>();

            _agent.speed = settings.Speed;
            
            _playerMoveController.Init(new MoveControllerData()
            {
                InputCatcher = inputCatcher,
                Agent = _agent,
                Camera = _camera,
                CameraPositon = topDawnCameraPositon,
                Animator = animator,
                Model = model
            });
            _playerShootController.Init(new ShootControllerData()
            {
                InputCatcher = inputCatcher,
                Camera = _camera,
                CameraPositon = fpsCameraPosition,
                SpawnBulletPlace = spawnBulletPlace,
                BulletPrefab = settings.BulletPrefab,
                ShootForce = settings.ShootForce,
                FireRate = settings.FireRate,
                Animator = animator,
                Model = model
            });

            moveStateOnButton.onClick.AddListener(ToggleMoveController);
        }

        private void Start()
        {
            ToggleMoveController();
        }

        public void ToggleMoveController()
        {
            _agent.isStopped = false;
            
            _playerMoveController.Enable();
            _playerShootController.Disable();
        }
        
        public void ToggleShootController(Vector3 lookAtPosition)
        {
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
            _agent.SetDestination(transform.position);

            _playerMoveController.Disable();
            _playerShootController.Enable(lookAtPosition);
        }
    }
}