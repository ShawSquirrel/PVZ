using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Attack_Princess : FsmState<APrincess>
    {
        protected override void OnEnter(IFsm<APrincess> fsm)
        {
            base.OnEnter(fsm);
            Attack(fsm).Forget();
        }

        public async UniTask Attack(IFsm<APrincess> fsm)
        {
            float time = Time.time;
            Log.Debug($"Start {time}");
            fsm.Owner._Anim.Play(EAnimState.Attack, false, () =>
            {
                Log.Debug($"End {Time.time - time}");
                ChangeState<Idle_Princess>(fsm);
            });
            await fsm.Owner.Attack();
        }
        
        
        protected override void OnUpdate(IFsm<APrincess> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (fsm.Owner._IsDie == true)
            {   
                ChangeState<Die_Princess>(fsm);
                return;
            }
        }
    }
}