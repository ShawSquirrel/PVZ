using System;
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
                else if (type == typeof(Princess_CaoYeYouYi))
                {
                    ret = APrincess.CreateInstance<Princess_CaoYeYouYi>(EPrincessType.CaoYeYouYi) as T;
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