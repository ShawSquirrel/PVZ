using TEngine;
using UnityEngine;

namespace GameLogic
{
    public partial class MapItem_Space : IEntity, IMapItem, IPos
    {
        public EMapItemType MapItemType => EMapItemType.Scpace;

        public Vector3 Position { get; set; }

        public Vector3 GetPos()
        {
            return Position;
        }

        public void SetPos(Vector3 pos)
        {
            Position = pos;
        }
    }

    public partial class MapItem_Space : ObjectBase
    {
        protected override void Release(bool isShutdown)
        {
        }

        public static MapItem_Space Create()
        {
            MapItem_Space ret = MemoryPool.Acquire<MapItem_Space>();
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>("MapItem"));
            target.GetComponent<MeshRenderer>().material.color = Color.grey;
            ret.Initialize("MapItem_Space", target);
            return ret;
        }
    }
}