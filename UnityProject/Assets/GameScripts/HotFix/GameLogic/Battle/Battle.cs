using System;
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
    
    [Serializable]
    public class Battle : Singleton<Battle>
    {
        public PlantSystem PlantSystem = new PlantSystem();
        public MapSystem MapSystem = new MapSystem();
        public ZomBiePlaceSystem ZomBiePlaceSystem = new ZomBiePlaceSystem();
        public SelectPrincessSystem SelectPrincessSystem = new SelectPrincessSystem();
        public BattleData BattleData;
        public EBattleType BattleType;
        

        protected override void Init()
        {
            base.Init();
            PlantSystem.Init();
            SelectPrincessSystem.Init();


            GameModule.UI.ShowUI<UI_SelectPrincess>(new List<EPrincessType>() { EPrincessType.CaoYeYouYi, EPrincessType.PeiKeLiMu });
            MapGenerate();
            
            ZomBiePlaceSystem.Init().Forget();
        }

        private void MapGenerate()
        {
            int[,] arr = new int[9, 6];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = j % 2 == 0 ? 0 : 1;
                }
            }

            MapSystem.MapGenerate(arr).Forget();
        }
    }
}