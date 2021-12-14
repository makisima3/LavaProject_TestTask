using UnityEngine;

namespace Code
{
    public static class TrigonometryUtils
    {
        public static Quaternion GetYRotation(Vector3 from, Vector3 to)
        {
            from.y = 0;
            to.y = 0;
            var direction = (to - from).normalized;
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            
            return Quaternion.Euler(0f,angle, 0f);
        }
    }
}