using GameConfig;

namespace GameLogic
{
    public class ZomBie_PeiKeLiMu : AZonBie
    {
        protected override void EndObjectInitialize()
        {
            base.EndObjectInitialize();
            AttributeDict.SetValue(EAttributeType.HitPoint, 100);
        }

        public override void Die()
        {
            base.Die();
            _Anim.Play(EAnimState.Die, false, ()=> PoolHelper.UnSpawn(this));
        }
    }
}