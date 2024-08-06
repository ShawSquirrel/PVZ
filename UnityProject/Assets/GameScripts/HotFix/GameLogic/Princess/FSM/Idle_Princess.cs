using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Idle_Princess : FsmState<APrincess>
    {
        public bool isAttackCheck = false;
        protected override void OnEnter(IFsm<APrincess> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner._Anim.Play(EAnimState.Idle);
            ChangeToAttack(fsm).Forget();
        }

        protected override void OnUpdate(IFsm<APrincess> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            
            if (fsm.Owner._IsDie == true)
            {
                ChangeState<Die_Princess>(fsm);
                return;
            }
            
            // if (fsm.Owner.AttackCheck() && isAttackCheck == false)
            // {
            //     Log.Info($"{GetType()} : {Time.time}");
            //     isAttackCheck = true;
            //     ChangeToAttack(fsm).Forget();
            // }
        }

        private async UniTask ChangeToAttack(IFsm<APrincess> fsm)
        {
            await UniTask.Delay(1000);
            if (fsm.Owner.AttackCheck())
            {
                ChangeState<Attack_Princess>(fsm);
            }
        }
    }
}