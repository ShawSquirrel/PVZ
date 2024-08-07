using System;
using GameConfig;
using Spine;

namespace GameLogic
{
    public interface IAnimComponent
    {
        public void Play(EAnimState animState, bool loop = true, Action onComplete = null);
        public void Pause();
        public void Continue();
        public void ResetAnim();
        public void SetAnimSpeed(float speed);
        public Animation GetCurrentAnimation(float track);
    }
}