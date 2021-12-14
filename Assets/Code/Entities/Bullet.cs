using System;
using Code.Interfaces;
using UnityEngine;

namespace Code.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float pushForce = 10f;
        [SerializeField] private GameObject view;
        [SerializeField] private Rigidbody rigidbody;

        public void Init(Vector3 target, float force)
        {
            transform.LookAt(target);

            transform.SetParent(null);
            rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);

            gameObject.SetActive(true);
            Destroy(gameObject, 5f);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<IArrowHandler>(out var handler))
            {
                var direction = transform.forward.normalized;

                Instantiate(view, transform.position, transform.rotation, other.transform);
                handler.Handl(direction * pushForce);

                Destroy(gameObject);
            }
        }
    }
}