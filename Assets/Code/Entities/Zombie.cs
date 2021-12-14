using System;
using System.Collections.Generic;
using Code.Interfaces;
using UnityEngine;
using UnityEngineInternal;

namespace Code.Entities
{
    [RequireComponent(typeof(CharacterController))]
    public class Zombie : MonoBehaviour
    {
        [SerializeField] private List<BodyPart> bodyParts;
        [SerializeField] private Point firstPoint;
        [SerializeField] private float speed = 5f;

        private Animator _animator;
        private CharacterController _characterController;
        private Point _curentPoint;
        private bool _isMove;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _curentPoint = firstPoint;
            _isMove = true;
        }

        private void Update()
        {
            if(!_isMove)
                return;

            transform.rotation = TrigonometryUtils.GetYRotation(transform.position, _curentPoint.transform.position);
            _characterController.Move(transform.forward * speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _curentPoint.transform.position) <= 1f)
            {
                _curentPoint = _curentPoint.Next;
            }
            
        }

        public void Death()
        {
            _characterController.enabled = false;
            _isMove = false;
            _animator.enabled = false;

            foreach (var bodyPart in bodyParts)
            {
                bodyPart.Rigidbody.isKinematic = false;
            }
        }
        
    }
}