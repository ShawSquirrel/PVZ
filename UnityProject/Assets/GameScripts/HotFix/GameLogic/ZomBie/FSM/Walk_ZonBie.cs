using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Walk_ZonBie : FsmState<AZonBie>
    {
        protected override void OnEnter(IFsm<AZonBie> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner._Anim.Play(EAnimState.Walk);
            fsm.Owner._Rigid.velocity = new Vector2(-0.5f, 0);
        }

        protected override void OnUpdate(IFsm<AZonBie> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (fsm.Owner._IsDie == true)
            {
                ChangeState<Die_ZonBie>(fsm);
                return;
            }

            if (fsm.Owner.AttackCheck())
            {
                ChangeState<Attack_ZonBie>(fsm);
                return;
            }
        }
    }
}