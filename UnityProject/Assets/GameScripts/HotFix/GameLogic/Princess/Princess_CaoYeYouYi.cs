namespace GameLogic
{
    public class Princess_CaoYeYouYi : APrincess, ICanPlant
    {
        public override EPrincessType PrincessType => EPrincessType.CaoYeYouYi;

        public void Plant(ICanPlanted canPlanted)
        {
        }
    }
}