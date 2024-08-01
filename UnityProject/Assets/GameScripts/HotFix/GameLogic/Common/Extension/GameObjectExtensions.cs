using UnityEngine;

namespace GameLogic
{
    public static class GameObjectExtensions
    {
        public static void SetActiveSelf(this GameObject self, bool isActive)
        {
            if (self.activeSelf == isActive) return;
                
            self.SetActive(isActive);
        }
    }
}