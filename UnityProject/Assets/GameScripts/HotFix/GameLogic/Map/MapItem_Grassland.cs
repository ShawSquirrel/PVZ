using TEngine;
using UnityEngine;

namespace GameLogic
{
    public partial class MapItem_Grassland : AMapItem, ICanPlanted
    {
        public EMapItemType MapItemType => EMapItemType.Grassland;

        protected override void MapItemInitialize()
        {
            base.MapItemInitialize();
            SetColor(Color.green);
        }

        public void Planted(ICanPlant canPlant)
        {
        }

        public static MapItem_Grassland CreateInstance()
        {
            MapItem_Grassland ret = MemoryPool.Acquire<MapItem_Grassland>();
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>("MapItem"));
            ret.Initialize($"{nameof(MapItem_Grassland)}", target);
            return ret;
        }
    }
}