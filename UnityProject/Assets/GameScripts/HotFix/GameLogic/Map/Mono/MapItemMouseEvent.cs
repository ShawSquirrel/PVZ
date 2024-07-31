using System;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class MapItemMouseEvent : MonoBehaviour
    {
        public Vector2Int MapItemIndex;
        private void OnMouseEnter()
        {
            // Log.Debug($"MapItemMouseEvent :: OnMouseEnter {transform.name}");
        }

        private void OnMouseDown()
        {
            Log.Debug($"MapItemMouseEvent :: OnMouseDown {transform.name}");
            PlantSystem plantSystem = Battle.Instance.PlantSystem;
            if (plantSystem.CanPlant == true)
            {
                plantSystem.Plant(MapItemIndex);
            }
        }
        
    }
}