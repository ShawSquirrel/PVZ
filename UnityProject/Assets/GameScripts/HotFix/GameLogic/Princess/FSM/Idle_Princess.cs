﻿using Cysharp.Threading.Tasks;
using GameConfig;
using TEngine;

namespace GameLogic
{
    public class Idle_Princess : FsmState<AActor>
    {
        protected override void OnEnter(IFsm<AActor> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner._Anim.Play(EAnimState.Idle);

            Change(fsm).Forget();
        }

        public async UniTask Change(IFsm<AActor> fsm)
        {
            await UniTask.Delay(3000);
            ChangeState<Attack_Princess>(fsm);
        }
    }
}