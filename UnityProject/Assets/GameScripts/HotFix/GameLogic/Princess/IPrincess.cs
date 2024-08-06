using GameConfig;

namespace GameLogic
{
    public interface IPrincess : IEntity
    {
        public EPrincessType PrincessType { get; }
    }
}