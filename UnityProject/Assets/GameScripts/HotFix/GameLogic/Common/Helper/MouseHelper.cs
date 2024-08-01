using TEngine;
using UnityEngine;

namespace GameLogic
{
    public static class MouseHelper
    {
        public static Vector3 S_LastHitPoint;

        public static Vector3 GetMousePointToWorldPoint()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;

            // 从摄像机创建一条通过鼠标位置的射线
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);

            // 创建一个用于存储射线检测结果的变量
            RaycastHit hit;

            // 如果射线击中了某个物体
            if (Physics.Raycast(ray, out hit, 100))
            {
                // 获取击中位置的3D坐标
                Vector3 hitPoint = hit.point;

                // 打印或处理击中位置
                // Log.Debug("Mouse position in 3D world: " + hitPoint);
                S_LastHitPoint = hitPoint;
            }

            // Log.Warning("Ray cast lost");
            return S_LastHitPoint;
        }
    }
}