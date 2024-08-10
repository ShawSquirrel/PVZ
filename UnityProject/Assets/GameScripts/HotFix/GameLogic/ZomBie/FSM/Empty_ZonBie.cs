using GameConfig;
using TEngine;

namespace GameLogic
{
    public class Empty_ZonBie : ZonBieState
    {
        protected override void OnEnter(IFsm<AZonBie> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner._Anim.Play(EAnimState.Idle);
            fsm.Owner._Anim.Pause();
        }
        
        
        protected override void OnUpdate(IFsm<AZonBie> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (fsm.Owner._IsDie == false)
            {
                ChangeState<Idle_ZonBie>(fsm);
            }
        }

        protected override void OnLeave(IFsm<AZonBie> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            fsm.Owner._Anim.Continue();
        }
    }
}