using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;

namespace GameLogic
{
    public class Idle_ZonBie : FsmState<AZonBie>
    {
        protected override void OnEnter(IFsm<AZonBie> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner._Anim.Play(EAnimState.Idle);

            Change(fsm).Forget();
        }

        public async UniTask Change(IFsm<AZonBie> fsm)
        {
            await UniTask.Delay(3000);
            ChangeState<Attack_ZonBie>(fsm);
        }
    }
}