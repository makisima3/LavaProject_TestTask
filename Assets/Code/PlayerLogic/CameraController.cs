using System;
using UnityEngine;

namespace Code.PlayerLogic
{
    public class CameraController : MonoBehaviour
    {
        private Transform _target;
        private float _speed;
        private Vector3 _offset;

        private bool _isFollow;

        private Camera _camera;

        public Camera Camera => _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Init(float followerSpeed, Transform target, Vector3 offset)
        {
            _speed = followerSpeed;
            _target = target;
            _offset = offset;
            ToggleFollow(true);
        }

        public void ToggleFollow(bool value)
        {
            _isFollow = value;
        }

        private void Update()
        {
            if(!_isFollow)
                return;
            
            transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _speed * Time.deltaTime);
        }
    }
}