using GameConfig;
using TEngine;

namespace GameLogic
{
    public class Empty_Princess : PrincessState
    {
        protected override void OnEnter(IFsm<APrincess> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner._Anim.Play(EAnimState.Idle);
            fsm.Owner._Anim.Pause();
        }
        
        
        protected override void OnUpdate(IFsm<APrincess> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (fsm.Owner._IsDie == false)
            {
                ChangeState<Idle_Princess>(fsm);
            }
        }

        protected override void OnLeave(IFsm<APrincess> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            fsm.Owner._Anim.Continue();
        }
    }
}