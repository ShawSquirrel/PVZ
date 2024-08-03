using Cysharp.Threading.Tasks;
using GameConfig;
using UnityEngine;

namespace GameLogic
{
    public class Princess_CaoYeYouYi : APrincess
    {
        public override EPrincessType PrincessType => EPrincessType.CaoYeYouYi;

        protected override void EndObjectInitialize()
        {
            base.EndObjectInitialize();
            _AttributeDict.SetValue(EAttributeType.HitPoint, 50);
        }

        public override bool Plant(MapData mapData)
        {
            return true;
        }


        public override bool AttackCheck()
        {
            Vector3 origin = _TF.transform.position;
            Vector3 direction = _TF.right;

            return RayHelper.RayCheck(origin, direction, 10f, 1 << LayerMask.NameToLayer("ZonBie"), Color.yellow);
        }

        public override async UniTask Attack()
        {
            await base.Attack();
            await UniTask.Delay(300);
            Bullet bullet = PoolHelper.Spawn<Bullet>();
            bullet._TF.position    = _TF.Find("AttackPoint").position;
            bullet._Rigid.velocity = Vector3.right * 3;
        }
    }
}