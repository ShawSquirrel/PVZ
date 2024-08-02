using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Bullet : ASkill, ITrigger
    {
        public Trigger3DEvent _Trigger3DEvent { get; set; }

        protected override void EndObjectInitialize()
        {
            base.EndObjectInitialize();
            
            _AttributeDict.SetValue(EAttributeType.Attack, 40);
            
            _Trigger3DEvent                      =  _TF.Find("Body").gameObject.AddComponent<Trigger3DEvent>();
            _Trigger3DEvent._Entity              =  this;
            _Trigger3DEvent.TriggerStay3DAction  += OnTriggerStay3DAction;
            _Trigger3DEvent.TriggerEnter3DAction += OnTriggerEnter3DAction;
            _Trigger3DEvent.TriggerExit3DAction  += OnTriggerExit3DAction;
        }

        protected virtual void OnTriggerEnter3DAction(Collider collider)
        {
            Trigger3DEvent trigger3DEvent = collider.GetComponent<Trigger3DEvent>();
            if (trigger3DEvent == null) return;

            if (trigger3DEvent._Entity is AZonBie zonBie)
            {
                Log.Debug($"造成伤害 :: {zonBie._TF.name}");
                PoolHelper.UnSpawn(this);
            }
        }

        protected virtual void OnTriggerExit3DAction(Collider collider)
        {
        }

        protected virtual void OnTriggerStay3DAction(Collider collider)
        {
        }


        protected override void OnSpawn()
        {
            base.OnSpawn();
            _Obj.SetActiveSelf(true);
        }

        protected override void OnUnSpawn()
        {
            base.OnUnSpawn();
            _Obj.SetActiveSelf(false);
        }

        protected override void Release(bool isShutdown)
        {
            base.Release(isShutdown);
            Object.Destroy(_Obj);
        }

        public static Bullet CreateInstance()
        {
            Bullet ret = MemoryPool.Acquire<Bullet>();
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>("Skill_Bullet"));
            ret.Initialize_Out(target);
            return ret;
        }
    }
}