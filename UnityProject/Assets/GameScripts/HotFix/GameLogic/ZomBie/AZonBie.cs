using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class AZonBie : ObjectBase, IEntity, IZomBie, IUnit, IAnim, IMove, ITrigger, IAttribute, IDie
    {
        public GameObject _Obj { get; set; }
        public Transform _TF { get; set; }
        public IAnimComponent _Anim { get; set; }
        public Rigidbody _Rigid { get; set; }
        public Trigger3DEvent _Trigger3DEvent { get; set; }
        public AttributeDictionary AttributeDict { get; set; } = new AttributeDictionary();


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
            _Anim.Play(EAnimState.Walk);
        }

        protected override void OnUnSpawn()
        {
            base.OnUnSpawn();
            _Obj.SetActiveSelf(false);
        }

        protected override void EndObjectInitialize()
        {
            _Obj            = Target as GameObject;
            _TF             = _Obj.transform;
            _Anim           = _TF.Find("Body").GetComponent<IAnimComponent>();
            _Rigid          = _Obj.GetComponent<Rigidbody>();
            _Rigid.velocity = new Vector2(-0.5f, 0);

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

            if (trigger3DEvent._Entity is ASkill skill)
            {
                if (skill is IAttribute attribute)
                {
                    float atk = attribute.AttributeDict.GetValue(EAttributeType.Attack);
                    
                    AttributeDict.AddValue(EAttributeType.HitPoint, -atk);
                    
                    Log.Debug($"被伤害 :: {skill._TF.name}  下降 :: {atk}  剩余 :: {AttributeDict.GetValue(EAttributeType.HitPoint)}");


                    if (AttributeDict.GetValue(EAttributeType.HitPoint) <= 0)
                    {
                        Die();
                    }
                }
            }
        }

        protected virtual void OnTriggerExit3DAction(Collider collider)
        {
        }

        protected virtual void OnTriggerStay3DAction(Collider collider)
        {
        }

        public static T CreateInstance<T>(EZombieType zombieType) where T : ObjectBase, new()
        {
            T ret = MemoryPool.Acquire<T>();
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>($"ZomBie_{zombieType}"));
            ret.Initialize_Out(target);
            return ret;
        }

        public virtual void Die()
        {
        }
    }
}