using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class AMapItem : ObjectBase, IEntity, IMapItem, IUnit
    {
        public GameObject Obj { get; set; }
        public Transform TF { get; set; }
        public virtual EMapItemType MapItemType { get; }

        protected override void Release(bool isShutdown)
        {
        }

        protected override void EndObjectInitialize()
        {
            Obj ??= Target as GameObject;
            TF  ??= Obj.transform;
            MapItemInitialize();
        }

        protected virtual void MapItemInitialize()
        {
            
        }
        public void SetColor(Color color)
        {
            Obj.GetComponent<SpriteRenderer>().color = color;
        }
        
        public static T CreateInstance<T>() where T : ObjectBase, new()
        {
            T ret = MemoryPool.Acquire<T>();
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>("MapItem"));
            ret.Initialize_Out($"{typeof(T).Name}", target);
            return ret;
        }
    }
}