using TEngine;

namespace GameLogic
{
    public interface IActorFSM<T> where T : class
    {
        public IFsm<T> _FSM { get; set; }
    }
}