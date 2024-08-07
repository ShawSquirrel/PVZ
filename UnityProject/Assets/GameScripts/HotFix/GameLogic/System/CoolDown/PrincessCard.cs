using GameConfig;

namespace GameLogic
{
    public class PrincessCard
    {
        public EPrincessType PrincessType;
        public float CoolDown;
        public float MaxCoolDown;
        public int Cost;

        public static PrincessCard ToObject(VPrincessCard princessCard)
        {
            PrincessCard card = new PrincessCard
                                {
                                    PrincessType = princessCard.PrincessType,
                                    CoolDown     = princessCard.CoolDown,
                                    MaxCoolDown  = princessCard.MaxCoolDown,
                                    Cost         = princessCard.Cost,
                                };
            return card;
        }
    }
}