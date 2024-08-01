﻿using System;
using GameConfig;

namespace GameLogic
{
    public interface IAnimComponent
    {
        public void Play(EAnimState animState, bool loop = true, Action OnComplete = null);
    }
}