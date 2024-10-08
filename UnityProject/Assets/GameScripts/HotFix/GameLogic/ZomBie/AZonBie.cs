﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class AZonBie : ObjectBaseWithInstance, IEntity, IZomBie, IUnit, IAnim, IMove, ITrigger, IAttribute, IDie, IActorFSM<AZonBie>, IAttack
    {
        public virtual EZombieType ZombieType { get; }

        public GameObject _Obj { get; set; }
        public Transform _TF { get; set; }
        public IAnimComponent _Anim { get; set; }
        public Rigidbody _Rigid { get; set; }
        public Trigger3DEvent _Trigger3DEvent { get; set; }
        public AttributeDictionary _AttributeDict { get; set; } = new AttributeDictionary();
        public IFsm<AZonBie> _FSM { get; set; }
        public bool _IsDie { get; set; } = false;


        protected override void EndObjectInitialize()
        {
            _Obj = Target as GameObject;
            _TF = _Obj.transform;
            _Anim = _TF.Find("Body").GetComponent<IAnimComponent>();
            _Rigid = _Obj.GetComponent<Rigidbody>();

            _FSM = GameModule.Fsm.CreateFsm($"{ZombieType} {GetHashCode()}", this, new List<FsmState<AZonBie>>()
            {
                new Empty_ZonBie(),
                new Idle_ZonBie(),
                new Attack_ZonBie(),
                new Walk_ZonBie(),
                new Die_ZonBie(),
            });
            _FSM.Start<Empty_ZonBie>();
            
            _Trigger3DEvent = _TF.Find("Body").gameObject.AddComponent<Trigger3DEvent>();
            _Trigger3DEvent._Entity = this;
            _Trigger3DEvent.TriggerStay3DAction += OnTriggerStay3DAction;
            _Trigger3DEvent.TriggerEnter3DAction += OnTriggerEnter3DAction;
            _Trigger3DEvent.TriggerExit3DAction += OnTriggerExit3DAction;

            _IsDie = true;
        }

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
            _IsDie = false;
        }

        protected override void OnUnSpawn()
        {
            base.OnUnSpawn();
            _Obj.SetActiveSelf(false);
            _IsDie = true;
        }


        protected virtual void OnTriggerEnter3DAction(Collider collider)
        {
            Trigger3DEvent trigger3DEvent = collider.GetComponent<Trigger3DEvent>();
            if (trigger3DEvent == null) return;

            if (trigger3DEvent._Entity is ASkill skill)
            {
                if (skill is IAttribute attribute)
                {
                    float atk = attribute._AttributeDict.GetValue(EAttributeType.Attack);

                    _AttributeDict.AddValue(EAttributeType.HitPoint, -atk);

                    Log.Debug($"被伤害 :: {skill._TF.name}  下降 :: {atk}  剩余 :: {_AttributeDict.GetValue(EAttributeType.HitPoint)}");


                    if (_AttributeDict.GetValue(EAttributeType.HitPoint) <= 0)
                    {
                        _IsDie = true;
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

        public override GameObject GetInstance()
        {
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>($"ZomBie_{ZombieType}"));

            return target;
        }

        public virtual void Die()
        {
        }

        public virtual bool AttackCheck()
        {
            return false;
        }

        public virtual async UniTask Attack()
        {
            await UniTask.CompletedTask;
        }
    }
}