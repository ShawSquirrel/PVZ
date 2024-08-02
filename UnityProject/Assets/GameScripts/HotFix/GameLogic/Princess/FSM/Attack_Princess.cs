using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;

namespace GameLogic
{
    public class Attack_Princess : FsmState<APrincess>
    {
        protected override void OnEnter(IFsm<APrincess> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner._Anim.Play(EAnimState.Attack);
            Change(fsm).Forget();
        }

        public async UniTask Change(IFsm<APrincess> fsm)
        {
            await UniTask.Delay(3000);
            ChangeState<Idle_Princess>(fsm);
        }
    }
}