using System;
using GameConfig;

namespace GameLogic
{
    public interface IAnimComponent
    {
        public void Play(EAnimState animState, bool loop = true, Action OnComplete = null);
        public void Pause();
        public void Continue();
        public void Reset();
        public void SetAnimSpeed(float speed);
    }
}