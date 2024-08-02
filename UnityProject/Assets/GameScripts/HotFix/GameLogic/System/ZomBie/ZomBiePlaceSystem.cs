using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class ZomBiePlaceSystem : IZomBiePlaceSystem
    {
        public Dictionary<int, Vector3> strikePointDict = new Dictionary<int, Vector3>();
        public async UniTask Init()
        {
            for (int i = 0; i < 6; i++)
            {
                strikePointDict.Add(i, new Vector3(10, -i));
            }

            await UniTask.Delay(2000);
            PlaceZomBie(EZombieType.PeiKeLiMu, strikePointDict[2]);
            await UniTask.CompletedTask;
        }
        
        public void PlaceZomBie(EZombieType zombieType, Vector3 pos)
        {
            AZonBie zonBie = PoolHelper.Spawn<ZomBie_PeiKeLiMu>();
            zonBie._TF.position = pos;
        }
    }
}