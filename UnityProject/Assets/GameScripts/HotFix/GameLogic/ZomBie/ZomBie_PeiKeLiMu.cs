using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class ZomBie_PeiKeLiMu : AZonBie
    {
        public override EZombieType ZombieType => EZombieType.PeiKeLiMu;

        protected override void EndObjectInitialize()
        {
            base.EndObjectInitialize();
            _AttributeDict.SetValue(EAttributeType.HitPoint, 100);
            _AttributeDict.SetValue(EAttributeType.Attack, 30);
        }

        public override void Die()
        {
            base.Die();
            PoolHelper.UnSpawn(this);
        }

        public override bool AttackCheck()
        {
            // 定义射线的起点为摄像机的当前位置
            Vector3 origin = _TF.transform.position;
            // 定义射线的方向为摄像机向前看的方向
            Vector3 direction = -_TF.right;

            // 存储射线碰撞结果的变量
            Debug.DrawLine(origin, origin + direction * 1f, Color.red);
            // 执行射线检测
            if (Physics.Raycast(origin, direction, 1f, 1 << LayerMask.NameToLayer("Princess")))
            {
                return true;
            }

            return false;
        }

        public override async UniTask Attack()
        {
            await UniTask.Delay(300);
            Log.Info($"{GetType()} Attack ");
            Vector3 origin = _TF.transform.position;
            Vector3 direction = -_TF.right;
            Debug.DrawLine(origin, origin + direction * 1f, Color.red);
            var gather = RayHelper.Raycast(origin, direction, 1f, 1 << LayerMask.NameToLayer("Princess"));

            if (gather.isCast)
            {
                var reference = gather.hitInfo.transform.GetComponent<Reference>();
                if (reference != null && reference.Entity is APrincess princess)
                {
                    princess.Damage(_AttributeDict.GetValue(EAttributeType.Attack));
                }
            }

            await UniTask.Yield();
        }
    }
}