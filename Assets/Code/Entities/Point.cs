using UnityEngine;

namespace Code.Entities
{
    public class Point : MonoBehaviour
    {
        [SerializeField] private Point next;

        public Point Next => next;
    }
}