using System;
using Code.Interfaces;
using UnityEngine;
using UnityEngineInternal;

namespace Code.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class BodyPart : MonoBehaviour, IArrowHandler
    {
        private Zombie _parent;
        private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _parent = GetComponentInParent<Zombie>();
        }

        public void Handl(Vector3 force)
        {
            _parent.Death();
            _rigidbody.AddForce(force,ForceMode.Impulse);
        }
    }
}