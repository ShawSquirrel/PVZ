using TEngine;
using UnityEngine;

namespace GameLogic
{
    public partial class MapItem_Space : AMapItem
    {
        public EMapItemType MapItemType => EMapItemType.Scpace;
        
        public static MapItem_Space CreateInstance()
        {
            MapItem_Space ret = MemoryPool.Acquire<MapItem_Space>();
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>("MapItem"));
            ret.Initialize($"{nameof(MapItem_Space)}", target);
            return ret;
        }
    }
    
}