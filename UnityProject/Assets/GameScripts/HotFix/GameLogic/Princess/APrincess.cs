using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class APrincess : ObjectBase, IPrincess, IUnit, IPlant, IAnim, IActorFSM<APrincess>, IAttribute, IAttack, IDie, IDamage, IBoxCollider
    {
        public GameObject _Obj { get; set; }
        public Transform _TF { get; set; }
        public IAnimComponent _Anim { get; set; }
        public IFsm<APrincess> _FSM { get; set; }
        public AttributeDictionary _AttributeDict { get; set; } = new AttributeDictionary();

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
            DisableCollider();
            _Anim.Play(EAnimState.Idle);
            _Anim.Pause();
            _Obj.SetActiveSelf(true);
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
            _Anim = _TF.Find("Body").GetComponent<IAnimComponent>();
            _TF.Find("Body").gameObject.AddComponent<Reference>().Entity = this;
            _BoxCollider = _TF.Find("Body").GetComponent<BoxCollider>();
        }

        public static T CreateInstance<T>(EPrincessType princessType) where T : ObjectBase, new()
        {
            T ret = MemoryPool.Acquire<T>();
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>($"Princess_{princessType}"));
            ret.Initialize_Out(target);
            return ret;
        }

        public virtual bool Plant(MapData mapData)
        {
            return false;
        }

        public virtual void PlantCallBack(MapData mapData)
        {
            _Anim.ResetAnim();
            _Anim.Continue();
            _FSM = GameModule.Fsm.CreateFsm($"{PrincessType.ToString()} {GetHashCode()}", this, new List<FsmState<APrincess>>()
            {
                new Attack_Princess(),
                new Idle_Princess(),
                new Die_Princess(),
            });
            _FSM.Start<Idle_Princess>();
            _IsDie = false;
            EnableCollider();
            

            _TF.transform.position = mapData._MapItem._TF.position;
        }

        public virtual bool AttackCheck()
        {
            return false;
        }

        public virtual async UniTask Attack()
        {
        }

        public bool _IsDie { get; set; }

        public virtual void Die()
        {
            _Anim.ResetAnim();
            GameModule.Fsm.DestroyFsm<APrincess>(_FSM.Name);
        }

        public void Damage(float attack)
        {
            _AttributeDict.AddValue(EAttributeType.HitPoint, attack * -1);

            if ((int)_AttributeDict.GetValue(EAttributeType.HitPoint) <= 0)
            {
                _IsDie = true;
            }
        }

        public BoxCollider _BoxCollider { get; set; }

        public void EnableCollider()
        {
            _BoxCollider.enabled = true;
        }

        public void DisableCollider()
        {
            _BoxCollider.enabled = false;
        }
    }
}