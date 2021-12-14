using Code.PlayerLogic;
using UnityEngine;

namespace Code.Entities
{
    public class PrepairForShootTrigger : MonoBehaviour
    {
        [SerializeField] private Transform lookAtPoint;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.ToggleShootController(lookAtPoint.position);
            }
        }
    }
}