using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class Attack_ZonBie : ZonBieState
    {
        protected override void OnEnter(IFsm<AZonBie> fsm)
        {
            base.OnEnter(fsm);

            Attack(fsm).Forget();
        }

        private async UniTask Attack(IFsm<AZonBie> fsm)
        {
            fsm.Owner._Rigid.velocity = Vector3.zero;
            Log.Debug($"Start {Time.time}");
            fsm.Owner._Anim.Play(EAnimState.Attack, false, () => Log.Debug($"End {Time.time}"));
            await fsm.Owner.Attack();
        }

        protected override void OnUpdate(IFsm<AZonBie> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (CheckDie(fsm)) return;
        }
    }
}