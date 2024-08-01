using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameBase;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    [Serializable]
    public class Battle : Singleton<Battle>
    {
        public PlantSystem PlantSystem = new PlantSystem();
        public MapSystem MapSystem = new MapSystem();
        
        protected override void Init()
        {
            base.Init();
            PlantSystem.Init();
            
            
            GameModule.UI.ShowUI<UI_SelectPrincess>(new List<EPrincessType>() { EPrincessType.CaoYeYouYi , EPrincessType.PeiKeLiMu});
            MapGenerate();

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