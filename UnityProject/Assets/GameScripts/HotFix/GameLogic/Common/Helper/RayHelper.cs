using UnityEngine;

namespace GameLogic
{
    public static class RayHelper
    {
        public static bool RayCheck(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
        {
            return RayCheck(origin, direction, maxDistance, layerMask, Color.red);
        }

        public static bool RayCheck(Vector3 origin, Vector3 direction, float maxDistance, int layerMask, Color color)
        {
            #if UNITY_EDITOR
            Debug.DrawLine(origin, origin + direction * maxDistance, color);
            #endif
            if (Physics.Raycast(origin, direction, maxDistance, layerMask))
            {
                return true;
            }

            return false;
        }
    }
}