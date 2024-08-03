using Cysharp.Threading.Tasks;

namespace GameLogic
{
    public interface IAttack
    {
        public bool AttackCheck();

        public UniTask Attack();
    }
}