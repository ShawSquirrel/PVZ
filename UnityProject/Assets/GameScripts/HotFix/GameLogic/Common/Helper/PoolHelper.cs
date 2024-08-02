using System;
using GameConfig;
using TEngine;

namespace GameLogic
{
    public static class PoolHelper
    {
        public static T Spawn<T>() where T : ObjectBase, new()
        {
            T ret = null;
            Type type = typeof(T);
            IObjectPool<T> objectPool = GameModule.ObjectPool.GetObjectPool<T>();
            objectPool ??= GameModule.ObjectPool.CreateSingleSpawnObjectPool<T>();
            
            if (objectPool.CanSpawn())
            {
                ret = objectPool.Spawn();
            }
            else
            {
                if (type == typeof(MapItem_Space))
                {
                    ret = AMapItem.CreateInstance<MapItem_Space>() as T;
                }
                else if (type == typeof(MapItem_Grassland))
                {
                    ret = AMapItem.CreateInstance<MapItem_Grassland>() as T;
                }
                
                
                
                else if (type == typeof(ActorCaoYeYouYi))
                {
                    ret = AActor.CreateInstance<ActorCaoYeYouYi>(EPrincessType.CaoYeYouYi) as T;
                }
                else if (type == typeof(ActorPeiKeLiMu))
                {
                    ret = AActor.CreateInstance<ActorPeiKeLiMu>(EPrincessType.PeiKeLiMu) as T;
                }
                
                
                else if (type == typeof(ZomBie_CaoYeYouYi))
                {
                    ret = AZonBie.CreateInstance<ZomBie_CaoYeYouYi>(EZombieType.CaoYeYouYi) as T;
                }
                else if (type == typeof(ZomBie_PeiKeLiMu))
                {
                    ret = AZonBie.CreateInstance<ZomBie_PeiKeLiMu>(EZombieType.PeiKeLiMu) as T;
                }

                else if (type == typeof(Bullet))
                {
                    ret = Bullet.CreateInstance() as T;
                }

                
                
                
                objectPool.Register(ret,true);
            }

            return ret;
        }
        public static void UnSpawn<T>(T needUnSpawn) where T : ObjectBase, new()
        {
            IObjectPool<T> objectPool = GameModule.ObjectPool.GetObjectPool<T>();

            if (objectPool == null)
            {
                Log.Error($"UnSpawn Error Type : {typeof(T)}");
                return;
            }
            objectPool.Unspawn(needUnSpawn);
        }
    }
}