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
            IObjectPool<T> _actorPool = GameModule.ObjectPool.GetObjectPool<T>();
            _actorPool ??= GameModule.ObjectPool.CreateSingleSpawnObjectPool<T>();
            
            if (_actorPool.CanSpawn())
            {
                ret = _actorPool.Spawn();
            }
            else
            {
                if (type == typeof(MapItem_Space))
                {
                    ret = MapItem_Space.CreateInstance() as T;
                }
                else if (type == typeof(MapItem_Grassland))
                {
                    ret = MapItem_Grassland.CreateInstance() as T;
                }
                _actorPool.Register(ret,true);
            }

            return ret;
        }
    }
}