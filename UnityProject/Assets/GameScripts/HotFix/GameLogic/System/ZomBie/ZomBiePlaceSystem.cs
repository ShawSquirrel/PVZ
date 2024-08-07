using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameConfig;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic
{
    public class ZomBiePlaceSystem : ASystem
    {
        public Dictionary<int, Vector3> strikePointDict = new Dictionary<int, Vector3>();

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < 6; i++)
            {
                strikePointDict.Add(i, new Vector3(10, -i));
            }
        }


        public async UniTask Init()
        {
            while (true)
            {
                await UniTask.Delay(1000);
                PlaceZomBie(EZombieType.PeiKeLiMu, strikePointDict[Random.Range(0, 6)]);
                await UniTask.CompletedTask;
            }
        }

        private void PlaceZomBie(EZombieType zombieType, Vector3 pos)
        {
            switch (zombieType)
            {
                case EZombieType.PeiKeLiMu:
                    AZonBie zonBie = PoolHelper.Spawn<ZomBie_PeiKeLiMu>();
                    zonBie._TF.position = pos;
                    break;
                case EZombieType.CaoYeYouYi:
                    break;
                case EZombieType.BingChuanJingHua:
                    break;
            }
        }
    }
}