using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class AZonBie : ObjectBase, IEntity, IZomBie, IUnit, IAnim, IMove
    {
        public GameObject _Obj { get; set; }
        public Transform _TF { get; set; }
        public IAnimComponent _Anim { get; set; }
        public Rigidbody2D _Rigid2D { get; set; }


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
            _Obj = Target as GameObject;
            _TF = _Obj.transform;
            _Anim = _TF.Find("Body").GetComponent<IAnimComponent>();
            _Rigid2D = _Obj.GetComponent<Rigidbody2D>();
            _Rigid2D.velocity = new Vector2(-2f, 0);
        }
        public static T CreateInstance<T>(EZombieType zombieType) where T : ObjectBase, new()
        {
            T ret = MemoryPool.Acquire<T>();
            GameObject target = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>($"ZomBie_{zombieType}"));
            ret.Initialize_Out(target);
            return ret;
        }

    }
}