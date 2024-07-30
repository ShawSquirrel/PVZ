using System;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class MapItemMouseEvent : MonoBehaviour
    {
        private void OnMouseEnter()
        {
            Log.Info($"MapItemMouseEvent :: OnMouseEnter {transform.name}");
        }

        private void OnMouseDown()
        {
            Log.Info($"MapItemMouseEvent :: OnMouseDown {transform.name}");
            GameEvent.Send("");
        }
        // private void OnMouseExit()
        // {
        //     Log.Info($"MapItemMouseEvent :: OnMouseExit {transform.name}");
        // }
    }
}