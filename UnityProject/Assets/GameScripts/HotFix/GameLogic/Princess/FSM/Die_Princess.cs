using GameConfig;
using TEngine;

namespace GameLogic
{
    public class Die_Princess : FsmState<APrincess>
    {
        protected override void OnEnter(IFsm<APrincess> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner._Anim.Play(EAnimState.Die, false, () => fsm.Owner.Die());
        }
    }
}