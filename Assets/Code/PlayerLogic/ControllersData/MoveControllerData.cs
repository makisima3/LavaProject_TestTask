using UnityEngine;
using UnityEngine.AI;

namespace Code.PlayerLogic.ControllersData
{
    public class MoveControllerData
    {
        public InputCatcher InputCatcher { get; set; }
        public NavMeshAgent Agent { get; set; }
        public CameraController Camera { get; set; }
        public Transform CameraPositon { get; set; }
        public Transform Model { get; set; }
        public Animator Animator{ get; set; }
    }
}