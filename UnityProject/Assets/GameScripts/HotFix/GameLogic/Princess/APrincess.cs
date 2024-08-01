using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class APrincess : ObjectBase, IPrincess, IUnit
    {
        public GameObject Obj { get; set; }
        public Transform TF { get; set; }

        public virtual EPrincessType PrincessType { get; }
        protected override void Release(bool isShutdown)
        {
            
        }

        protected override void EndObjectInitialize()
        {
            Obj = Target as GameObject;
            TF = Obj.transform;
        }
        
        public static T CreateInstance<T>(EPrincessType princessType) where T : ObjectBase, new()
        {
            T ret = MemoryPool.Acquire<T>();
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>($"Princess_{princessType}"));
            ret.Initialize_Out($"{typeof(T).Name}", target);
            return ret;
        }
    }
}