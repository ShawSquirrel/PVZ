using TEngine;
using UnityEngine;

namespace GameLogic
{
    public partial class MapItem_Space : AMapItem
    {
        public EMapItemType MapItemType => EMapItemType.Scpace;


        protected override void MapItemInitialize()
        {
            base.MapItemInitialize();
            SetColor(Color.yellow);
        }
    }
    
}