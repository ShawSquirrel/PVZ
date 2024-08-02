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
            fsm.Owner._Anim.Play(EAnimState.Attack, false, () => ChangeState<Idle_Princess>(fsm));
            await UniTask.Delay(300);
            GameObject bullet = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>("CaoYeYouYi_Bullet"));
            bullet.transform.position                 = fsm.Owner._TF.Find("AttackPoint").position;
            bullet.GetComponent<Rigidbody>().velocity = Vector3.right;
        }
    }
}