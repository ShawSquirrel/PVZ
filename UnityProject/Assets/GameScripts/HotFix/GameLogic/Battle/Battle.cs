using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameBase;
using GameConfig;
using TEngine;

namespace GameLogic
{
    public enum EBattleType
    {
        SelectPrincessCard,
        Battle
    }

    public class Battle : Singleton<Battle>
    {
        public PlantSystem PlantSystem = ASystem.CreateSystem<PlantSystem>();
        public MapSystem MapSystem = ASystem.CreateSystem<MapSystem>();
        public ZomBiePlaceSystem ZomBiePlaceSystem = ASystem.CreateSystem<ZomBiePlaceSystem>();
        public SelectPrincessSystem SelectPrincessSystem = ASystem.CreateSystem<SelectPrincessSystem>();
        public CoolDownSystem CoolDownSystem = ASystem.CreateSystem<CoolDownSystem>();
        public DiamondSystem DiamondSystem = ASystem.CreateSystem<DiamondSystem>();
        public EBattleType BattleType;


        #region Data

        public readonly List<SelectedPrincessCardData> _SelectedPrincessCardList = new List<SelectedPrincessCardData>();
        public readonly Dictionary<EPrincessType, OptionalPrincessCardData> _OptionalPrincessDict = new Dictionary<EPrincessType, OptionalPrincessCardData>();
        public int DiamondCount = 50;

        #endregion


        public void Init(int level)
        {
            Log.Info($"Load {level}");
        }


        protected override void Init()
        {
            base.Init();
            PlantSystem.Init();
            SelectPrincessSystem.Init();
            GameModule.UI.ShowUI<UI_SelectPrincess>(new List<EPrincessType>() { EPrincessType.CaoYeYouYi, EPrincessType.PeiKeLiMu });
            MapSystem.MapGenerate();
            ZomBiePlaceSystem.Init().Forget();
        }
    }
}