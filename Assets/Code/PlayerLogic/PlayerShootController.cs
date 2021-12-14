using System.Collections;
using Code.Entities;
using Code.PlayerLogic.ControllersData;
using DG.Tweening;
using UnityEngine;

namespace Code.PlayerLogic
{
    public class PlayerShootController : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;
        
        private InputCatcher _inputCatcher;
        private CameraController _camera;
        private Transform _cameraPlace;
        private Transform _spawnBulletPlace;
        private Bullet _bulletPrefab;
        private float _fireRate;
        private float _shootForce;
        private Animator _animator;

        private Coroutine _shootCorotine;
        private Vector3 targetPosition;
        private Transform _model;

        public void Init(ShootControllerData data)
        {
            _inputCatcher = data.InputCatcher;
            _camera = data.Camera;
            _cameraPlace = data.CameraPositon;
            _spawnBulletPlace = data.SpawnBulletPlace;
            _bulletPrefab = data.BulletPrefab;
            _fireRate = data.FireRate;
            _shootForce = data.ShootForce;
            _animator = data.Animator;
            _model = data.Model;
        }

        public void Enable(Vector3 lookAtPosition)
        {
            _inputCatcher.OnDownEvent.AddListener(StartShoot);
            _inputCatcher.OnUpEvent.AddListener(StopShoot);
            _inputCatcher.OnDragEvent.AddListener(ChangeTargetPosition);
            
            transform.rotation = TrigonometryUtils.GetYRotation(transform.position, lookAtPosition);
            
            _camera.ToggleFollow(false);
            _camera.transform.DOMove(_cameraPlace.position, 0.1f);
            _camera.transform.DORotateQuaternion(_cameraPlace.rotation, 0.1f);
            
            //model rotation fixes
            _model.DORotate(Vector3.up * 90, 0.1f);

            enabled = true;
        }

        public void Disable()
        {
            _inputCatcher.OnDownEvent.RemoveListener(StartShoot);
            _inputCatcher.OnUpEvent.RemoveListener(StopShoot);
            _inputCatcher.OnDragEvent.RemoveListener(ChangeTargetPosition);
            //model rotation fixes
            _model.DORotate(Vector3.up * 0, 0.1f);
            enabled = false;
        }

        private void ChangeTargetPosition(Vector3 target)
        {
            var ray = _camera.Camera.ScreenPointToRay(target);
                
            if (Physics.Raycast(ray, out var hit,float.MaxValue, groundMask))
            {
                targetPosition = hit.point;
            }
            else
            {
                targetPosition= ray.GetPoint(1000f);
            }
        }

        private void StartShoot(Vector3 target)
        {
            ChangeTargetPosition(target);
            _shootCorotine = StartCoroutine(Shoot());
        }

        private void StopShoot()
        {
            if (_shootCorotine != null)
            {
                StopCoroutine(_shootCorotine);
                _shootCorotine = null;
            }
        }

        private IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(1 / _fireRate);
                
                    //1 sec * animationTime * _firerate = Animation count to play in sec
                _animator.speed = 1 * 5  * _fireRate;
                _animator.SetTrigger("shoot");
                _model.rotation = TrigonometryUtils.GetYRotation(_model.position ,targetPosition) * Quaternion.Euler(Vector3.up * 90f);
                
                var bullet = Instantiate(_bulletPrefab.gameObject, _spawnBulletPlace).GetComponent<Bullet>();
                bullet.Init(targetPosition, _shootForce);
            }
        }
    }
}