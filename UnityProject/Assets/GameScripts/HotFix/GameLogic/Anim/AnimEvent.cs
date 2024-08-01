using System;

namespace GameLogic
{
    public struct AnimEvent
    {
        public Action OnComplete;
        public void Reset()
        {
            OnComplete = null;
        }
    }
}