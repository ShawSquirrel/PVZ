using GameConfig;

namespace GameLogic
{
    public class ActorCaoYeYouYi : AActor
    {
        public override EPrincessType PrincessType => EPrincessType.CaoYeYouYi;

        public override bool Plant(AMapItem mapItem)
        {
            return CanPlant(mapItem);
        }
    }
}