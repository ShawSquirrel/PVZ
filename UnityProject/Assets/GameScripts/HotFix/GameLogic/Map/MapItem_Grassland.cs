namespace GameLogic
{
    public class MapItem_Grassland : Entity, IMapItem, ICanPlanted
    {
        public EMapItemType MapItemType => EMapItemType.Grassland;
        public void Planted(ICanPlant canPlant)
        {
            
        }

        public MapItemMouseEvent MapItemMouseEvent { get; set; }
    }
}