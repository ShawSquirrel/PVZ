using GameConfig;
using TEngine;

namespace GameLogic
{
    public class OptionalPrincessCardData : IMemory
    {
        public EPrincessType PrincessType;
        public SelectedPrincessCardData SelectedPrincessCardData;
        public bool isSelected;

        public void Init(EPrincessType princessType)
        {
            PrincessType = princessType;
            SelectedPrincessCardData = MemoryPool.Acquire<SelectedPrincessCardData>();
            SelectedPrincessCardData.Init(princessType);
        }
        
        public void Clear()
        {
            PrincessType = EPrincessType.Null;
            MemoryPool.Release(SelectedPrincessCardData);
            isSelected = false;
        }
    }

    public class SelectedPrincessCardData : IMemory
    {
        public EPrincessType PrincessType;
        public float CoolDown;
        public float MaxCoolDown;
        public int Cost;

        public void Init(EPrincessType princessType)
        {
            var config = ConfigSystem.Instance.Tables.TPrincessCard;
            VPrincessCard princessCard = config.Get(princessType);
            PrincessType = princessCard.PrincessType;
            CoolDown = princessCard.CoolDown;
            MaxCoolDown = princessCard.MaxCoolDown;
            Cost = princessCard.Cost;
        }
        
        public void Clear()
        {
            PrincessType = EPrincessType.Null;
            CoolDown = -1;
            MaxCoolDown = -1;
            Cost = -1;
        }
    }
}