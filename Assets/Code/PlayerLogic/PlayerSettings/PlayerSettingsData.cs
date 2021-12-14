using Code.Entities;
using UnityEngine;

namespace Code.PlayerLogic.PlayerSettings
{
    public class PlayerSettingsData : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField,Tooltip("ShootsInSecond")] private float fireRate;
        [SerializeField] private float shootForce;
        [SerializeField] private Vector3 cameraOffset;
        [SerializeField] private Bullet bulletPrefab;
        
        public Bullet BulletPrefab => bulletPrefab;
        public float Speed => speed;
        public float ShootForce => shootForce;
        public float FireRate => fireRate;
        public Vector3 CameraOffset => cameraOffset;
    }
}