using Cysharp.Threading.Tasks;
using GameConfig;
using UnityEngine;

namespace GameLogic
{
    public class ZomBiePlaceSystem : IZomBiePlaceSystem
    {
        public async UniTask Init()
        {
            await UniTask.Delay(3000);
            PlaceZomBie(EZombieType.PeiKeLiMu, new Vector2Int(8,5));
            await UniTask.CompletedTask;
        }
        
        
        public void PlaceZomBie(EZombieType zombieType, Vector2Int index)
        {
            AZonBie zonBie = PoolHelper.Spawn<ZomBie_PeiKeLiMu>();
            zonBie._TF.position = GameApp.Instance.Battle.MapSystem._mapDataDict[index]._MapItem._TF.position;
        }
    }
}