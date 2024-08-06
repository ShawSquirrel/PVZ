using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Attack_ZonBie : FsmState<AZonBie>
    {
        private bool isAttackComplete;

        protected override void OnEnter(IFsm<AZonBie> fsm)
        {
            base.OnEnter(fsm);
            isAttackComplete          = true;
            fsm.Owner._Rigid.velocity = Vector3.zero;
        }

        private async UniTask Attack(IFsm<AZonBie> fsm)
        {
            isAttackComplete = false;
            fsm.Owner._Anim.Play(EAnimState.Attack, false, () => isAttackComplete = true);
            await fsm.Owner.Attack();

        }

        protected override void OnUpdate(IFsm<AZonBie> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (fsm.Owner._IsDie == true)
            {
                ChangeState<Die_ZonBie>(fsm);
                return;
            }

            if (isAttackComplete == false) return;

            if (fsm.Owner.AttackCheck())
            {
                Attack(fsm).Forget();
            }
            else
            {
                ChangeState<Walk_ZonBie>(fsm);
            }
        }
    }
}