using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class APrincess : ObjectBase, IPrincess, IUnit, IPlant, IAnim
    {
        public GameObject _Obj { get; set; }
        public Transform _TF { get; set; }
        public IAnimComponent Anim { get; set; }

        public virtual EPrincessType PrincessType { get; }

        protected override void Release(bool isShutdown)
        {
            if (_Obj != null)
            {
                Object.Destroy(_Obj);
            }
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            _Obj.SetActiveSelf(true);
            Anim.Play(EAnimState.Idle);
        }

        protected override void OnUnSpawn()
        {
            base.OnUnSpawn();
            _Obj.SetActiveSelf(false);
        }

        protected override void EndObjectInitialize()
        {
            _Obj = Target as GameObject;
            _TF = _Obj.transform;
            Anim = _TF.Find("Body").GetComponent<IAnimComponent>();
        }

        public static T CreateInstance<T>(EPrincessType princessType) where T : ObjectBase, new()
        {
            T ret = MemoryPool.Acquire<T>();
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>($"Princess_{princessType}"));
            ret.Initialize_Out(target);
            return ret;
        }

        public virtual bool Plant(AMapItem mapItem)
        {
            return false;
        }

        protected bool CanPlant(AMapItem mapItem)
        {
            _TF.transform.position = mapItem._TF.position;
            return true;
        }
    }
}