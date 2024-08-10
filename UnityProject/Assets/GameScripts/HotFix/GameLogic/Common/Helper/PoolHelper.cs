using System;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public static class PoolHelper
    {
        public static T Spawn<T>() where T : ObjectBaseWithInstance, new()
        {
            T ret;
            IObjectPool<T> objectPool = GameModule.ObjectPool.GetObjectPool<T>();
            objectPool ??= GameModule.ObjectPool.CreateSingleSpawnObjectPool<T>();

            if (objectPool.CanSpawn())
            {
                ret = objectPool.Spawn();
            }
            else
            {
                ret = MemoryPool.Acquire<T>();
                ret.Initialize_Out(ret.GetInstance());
                
                objectPool.Register(ret, true);
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

    public interface IGetInstance
    {
        public GameObject GetInstance();
    }
}