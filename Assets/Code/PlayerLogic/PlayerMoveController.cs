using System;
using System.Net.NetworkInformation;
using Code.PlayerLogic.ControllersData;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Code.PlayerLogic
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;

        private InputCatcher _inputCatcher;
        private NavMeshAgent _agent;
        private CameraController _camera;
        private Transform _cameraPlace;
        private Animator _animator;

        public void Init(MoveControllerData data)
        {
            _inputCatcher = data.InputCatcher;
            _agent = data.Agent;
            _camera = data.Camera;
            _cameraPlace = data.CameraPositon;
            _animator = data.Animator;
        }

        public void Enable()
        {
            _inputCatcher.OnDownEvent.AddListener(Move);
            _camera.ToggleFollow(true);
            _camera.transform.DORotateQuaternion(_cameraPlace.localRotation, 0.1f);

            enabled = true;
        }

        public void Disable()
        {
            _inputCatcher.OnDownEvent.RemoveListener(Move);
            enabled = false;
        }

        private bool _isMoving;

        private void Update()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (_isMoving)
                {
                    _animator.SetTrigger("idle");
                    _isMoving = false;
                }
            }
        }

        public void Move(Vector3 target)
        {
            var ray = _camera.Camera.ScreenPointToRay(target);

            if (Physics.Raycast(ray, out var hit, float.MaxValue, groundMask))
            {
                _agent.SetDestination(hit.point);
            }
            else
            {
                _agent.SetDestination(ray.GetPoint(1000f));
            }

            _animator.speed = 1;
            if (!_isMoving)
            {
                _animator.SetTrigger("run");
                _isMoving = true;
            }
        }
    }
}