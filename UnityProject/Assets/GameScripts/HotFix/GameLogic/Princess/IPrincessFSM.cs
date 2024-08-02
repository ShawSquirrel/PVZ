using TEngine;

namespace GameLogic
{
    public interface IPrincessFSM<T> where T : class
    {
        public IFsm<T> FSM { get; set; }
    }
}