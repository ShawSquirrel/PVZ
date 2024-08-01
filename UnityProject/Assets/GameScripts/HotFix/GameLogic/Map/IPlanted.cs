namespace GameLogic
{
    public interface IPlanted
    {
        public EMapItemType MapItemType { get;}
        public bool Planted();
    }
}