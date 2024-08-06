using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Walk_ZonBie : ZonBieState
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
            
            if (CheckDie(fsm)) return;
            
            if (fsm.Owner.AttackCheck())
            {
                ChangeState<Idle_ZonBie>(fsm);
                return;
            }
        }
    }
}