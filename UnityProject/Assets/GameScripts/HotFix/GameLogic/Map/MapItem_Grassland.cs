using TEngine;
using UnityEngine;

namespace GameLogic
{
    public partial class MapItem_Grassland : AMapItem
    {
        public EMapItemType MapItemType => EMapItemType.Grassland;

        protected override void MapItemInitialize()
        {
            base.MapItemInitialize();
            SetColor(Color.green);
        }

        public override bool Planted()
        {
            return true;
        }
    }
}