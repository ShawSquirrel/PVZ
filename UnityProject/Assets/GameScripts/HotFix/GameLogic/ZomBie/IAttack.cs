using Cysharp.Threading.Tasks;

namespace GameLogic
{
    public interface IAttack
    {
        public bool AttackCheck();

        public UniTask Attack();
    }

    public interface IDamage
    {
        // public bool AttackCheck();

        public void Damage(float attack);
    }
}