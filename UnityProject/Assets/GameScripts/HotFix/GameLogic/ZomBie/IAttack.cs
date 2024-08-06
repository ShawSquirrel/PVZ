using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameLogic
{
    public interface IAttack
    {
        public bool AttackCheck();

        public UniTask Attack();
    }

    public interface IDamage
    {
        public void Damage(float attack);
    }

    public interface IBoxCollider
    {
        public BoxCollider _BoxCollider { get; set; }
        public void EnableCollider();
        public void DisableCollider();
    }
}