using TEngine;
using UnityEngine;

namespace GameLogic
{
    public partial class MapItem_Grassland : IEntity, IMapItem, ICanPlanted, IPos
    {
        public EMapItemType MapItemType => EMapItemType.Grassland;
        public void Planted(ICanPlant canPlant)
        {
            
        }

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


    public partial class MapItem_Grassland : ObjectBase
    {
        protected override void Release(bool isShutdown)
        {
            
        }
    }
}