using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Die_ZonBie : FsmState<AZonBie>
    {
        protected override void OnEnter(IFsm<AZonBie> fsm)
        {
            base.OnEnter(fsm);
            
            fsm.Owner._Rigid.velocity = Vector3.zero;
            
            fsm.Owner._Anim.Play(EAnimState.Die, false, () => fsm.Owner.Die());
        }
    }
}