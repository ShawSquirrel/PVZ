namespace GameLogic
{
    public class ZomBie_PeiKeLiMu : AZonBie
    {
        protected override void EndObjectInitialize()
        {
            base.EndObjectInitialize();
            AttributeDict.SetValue(EAttributeType.HitPoint, 100);
        }
    }
}