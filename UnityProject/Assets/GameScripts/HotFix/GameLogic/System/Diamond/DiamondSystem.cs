namespace GameLogic
{
    public class DiamondSystem : ASystem
    {
        public int DiamondCount
        {
            get => Battle.Instance.DiamondCount;
            set => Battle.Instance.DiamondCount = value;
        }
    }
}