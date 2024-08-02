using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Attack_Princess : FsmState<AActor>
    {
        protected override void OnEnter(IFsm<AActor> fsm)
        {
            base.OnEnter(fsm);
            Attack(fsm).Forget();
        }

        public async UniTask Attack(IFsm<AActor> fsm)
        {
            fsm.Owner._Anim.Play(EAnimState.Attack, false, () => ChangeState<Idle_Princess>(fsm));
            await UniTask.Delay(300);
            Bullet bullet = PoolHelper.Spawn<Bullet>();
            bullet._TF.position                 = fsm.Owner._TF.Find("AttackPoint").position;
            bullet._Rigid.velocity = Vector3.right * 3;
        }
    }
}