using TEngine;

namespace GameLogic
{
    public class ZonBieState : FsmState<AZonBie>
    {
        public bool CheckDie(IFsm<AZonBie> fsm)
        {
            if (fsm.Owner._IsDie == true)
            {
                ChangeState<Die_ZonBie>(fsm);
            }

            return fsm.Owner._IsDie;
        }
    }
}