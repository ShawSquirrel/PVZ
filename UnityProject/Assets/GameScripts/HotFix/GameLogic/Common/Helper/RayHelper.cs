using UnityEngine;

namespace GameLogic
{
    public static class RayHelper
    {
        public static (bool isCast, RaycastHit hitInfo) Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
        {
            return Raycast(origin, direction, maxDistance, layerMask, Color.red);
        }

        public static (bool isCast, RaycastHit hitInfo) Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask, Color color)
        {
            #if UNITY_EDITOR
            Debug.DrawLine(origin, origin + direction * maxDistance, color);
            #endif
            if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, maxDistance, layerMask))
            {
                return (true, hitInfo);
            }

            return (false, default);
        }
    }
}