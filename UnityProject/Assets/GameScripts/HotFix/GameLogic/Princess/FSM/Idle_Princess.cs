using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;

namespace GameLogic
{
    public class Idle_Princess : FsmState<APrincess>
    {
        protected override void OnEnter(IFsm<APrincess> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner._Anim.Play(EAnimState.Idle);
        }

        protected override void OnUpdate(IFsm<APrincess> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            
            if (fsm.Owner._IsDie == true)
            {
                ChangeState<Die_Princess>(fsm);
                return;
            }
            
            if (fsm.Owner.AttackCheck())
            {
                ChangeState<Attack_Princess>(fsm);
            }
        }
    }
}