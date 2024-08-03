using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Attack_Princess : FsmState<APrincess>
    {
        private bool isAttackComplete;
        protected override void OnEnter(IFsm<APrincess> fsm)
        {
            base.OnEnter(fsm);
            isAttackComplete = true;
        }

        public async UniTask Attack(IFsm<APrincess> fsm)
        {
            isAttackComplete = false;
            fsm.Owner._Anim.Play(EAnimState.Attack, false, () => isAttackComplete = true);
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

            if (isAttackComplete == false) return;

            if (fsm.Owner.AttackCheck())
            {
                Attack(fsm).Forget();
            }
            else
            {
                ChangeState<Idle_Princess>(fsm);
            }
        }
    }
}