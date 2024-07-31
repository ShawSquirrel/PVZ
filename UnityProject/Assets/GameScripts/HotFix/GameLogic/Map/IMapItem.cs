namespace GameLogic
{
    public interface IMapItem : IMapItemMouseEvent
    {
        public EMapItemType MapItemType { get;}
    }
}