using TEngine;

namespace GameLogic
{
    public class MapItem_Space : Entity, IMapItem
    {
        public EMapItemType MapItemType => EMapItemType.Scpace;

        public MapItemMouseEvent MapItemMouseEvent { get; set; }
    }


}