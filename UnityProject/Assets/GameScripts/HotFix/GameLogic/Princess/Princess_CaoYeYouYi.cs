namespace GameLogic
{
    public class Princess_CaoYeYouYi : Entity, IPrincess, ICanPlant
    {
        public EPrincessType PrincessType => EPrincessType.CaoYeYouYi;

        public void Plant(ICanPlanted canPlanted)
        {
        }
    }
}