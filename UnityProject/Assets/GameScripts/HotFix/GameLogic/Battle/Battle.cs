using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameBase;
using GameConfig;
using TEngine;
using UnityEngine;

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

            
            TextAsset textAsset = GameModule.Resource.LoadAsset<TextAsset>("Map6x9");
            string mapStr = textAsset.text;
            string[] mapStrArr = mapStr.Trim('\n').Split('\n');
            int length = mapStrArr[0].Trim(',').Split(',').Length;
            int hieght = mapStrArr.Length; 

            for (int i = 0; i < hieght; i++)
            {
                for (int ii = 0; ii < length; ii++)
                {
                    string[] rowArr = mapStrArr[i].Trim(',').Split(',');
                    arr[ii, i] = int.Parse(rowArr[ii]);
                }
            }
            

            MapSystem.MapGenerate(arr).Forget();
        }
    }
}