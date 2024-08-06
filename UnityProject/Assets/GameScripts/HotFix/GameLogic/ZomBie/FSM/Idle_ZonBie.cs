using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Idle_ZonBie : ZonBieState
    {
        protected override void OnEnter(IFsm<AZonBie> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner._Anim.Play(EAnimState.Idle);
            fsm.Owner._Rigid.velocity = Vector3.zero;
            Check(fsm).Forget();
        }

        private async UniTask Check(IFsm<AZonBie> fsm)
        {
            await UniTask.Delay(300);
            if (fsm.Owner.AttackCheck())
            {
                ChangeState<Attack_ZonBie>(fsm);
            }
            else
            {
                ChangeState<Walk_ZonBie>(fsm);
            }
        }

        protected override void OnUpdate(IFsm<AZonBie> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (CheckDie(fsm)) return;
        }
    }
}