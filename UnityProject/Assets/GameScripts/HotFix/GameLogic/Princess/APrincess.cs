using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class APrincess : ObjectBaseWithInstance, IPrincess, IUnit, IPlant, IAnim, IActorFSM<APrincess>, IAttribute, IAttack, IDie, IDamage, IBoxCollider
    {
        public GameObject _Obj { get; set; }
        public Transform _TF { get; set; }
        public IAnimComponent _Anim { get; set; }
        public IFsm<APrincess> _FSM { get; set; }
        public AttributeDictionary _AttributeDict { get; set; } = new AttributeDictionary();

        public virtual EPrincessType PrincessType { get; }

        protected override void EndObjectInitialize()
        {
            _Obj = Target as GameObject;
            _TF = _Obj.transform;
            _Anim = _TF.Find("Body").GetComponent<IAnimComponent>();
            _TF.Find("Body").gameObject.AddComponent<Reference>().Entity = this;
            _BoxCollider = _TF.Find("Body").GetComponent<BoxCollider>();

            _FSM = GameModule.Fsm.CreateFsm($"{PrincessType.ToString()} {GetHashCode()}", this, new List<FsmState<APrincess>>()
            {
                new Empty_Princess(),
                new Attack_Princess(),
                new Idle_Princess(),
                new Die_Princess(),
            });
            _FSM.Start<Empty_Princess>();
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
            DisableCollider();
            _IsDie = true;
            _Obj.SetActiveSelf(true);
        }

        protected override void OnUnSpawn()
        {
            base.OnUnSpawn();
            _Obj.SetActiveSelf(false);
            _IsDie = true;
        }


        public override GameObject GetInstance()
        {
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>($"Princess_{PrincessType}"));
            return target;
        }

        public virtual bool Plant(MapData mapData)
        {
            return false;
        }

        public virtual void PlantCallBack(MapData mapData)
        {
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
            await UniTask.CompletedTask;
        }

        public bool _IsDie { get; set; }

        public virtual void Die()
        {
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