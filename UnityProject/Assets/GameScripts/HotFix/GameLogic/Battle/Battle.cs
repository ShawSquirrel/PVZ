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
        public EBattleType BattleType;


        #region Data

        
        public List<PrincessCard> _LeftPrincessCardList = new List<PrincessCard>();


        
        public Dictionary<EPrincessType, bool> _ToBeSelectedPrincessDict = new Dictionary<EPrincessType, bool>();
        // public List<EPrincessType> _SelectedPrincessList = new List<EPrincessType>();

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