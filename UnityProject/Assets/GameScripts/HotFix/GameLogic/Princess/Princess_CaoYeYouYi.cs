using GameConfig;

namespace GameLogic
{
    public class Princess_CaoYeYouYi : APrincess
    {
        public override EPrincessType PrincessType => EPrincessType.CaoYeYouYi;

        public override bool Plant(AMapItem mapItem)
        {
            return CanPlant(mapItem);
        }
    }
}