using Code.Entities;
using UnityEngine;

namespace Code.PlayerLogic.ControllersData
{
    public class ShootControllerData
    {
        public InputCatcher InputCatcher { get; set; }
        public CameraController Camera { get; set; }
        public Transform CameraPositon { get; set; }
        public Transform SpawnBulletPlace { get; set; }
        public Bullet BulletPrefab { get; set; }
        public float ShootForce { get; set; }
        public float FireRate{ get; set; }
        public Transform Model { get; set; }
        public Animator Animator{ get; set; }
    }
}