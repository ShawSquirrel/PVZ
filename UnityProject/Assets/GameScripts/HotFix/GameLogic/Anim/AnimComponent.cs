using System;
using System.Collections.Generic;
using System.Security.Policy;
using GameConfig;
using Sirenix.OdinInspector;
using Spine;
using Spine.Unity;
using TEngine;
using UnityEngine;
using Animation = Spine.Animation;

namespace GameLogic
{
    [ShowInInspector]
    public class AnimComponent : MonoBehaviour, IAnimComponent
    {
        private static Dictionary<EAnimState, string> S_AnimNameDict;

        public AnimEvent _Event;

        public string _NormalPrefix;
        public string _SkillPrefix;
        public EAnimState _AnimState;
        private Dictionary<EAnimState, string> _animNameDict = new Dictionary<EAnimState, string>();
        private SkeletonAnimation _anim;

        private void Awake()
        {
            if (S_AnimNameDict == null)
            {
                S_AnimNameDict = new Dictionary<EAnimState, string>();
                foreach (var anim in ConfigSystem.Instance.Tables.TAnim.DataList)
                {
                    S_AnimNameDict[anim.State] = anim.Name;
                }
            }

            _anim = GetComponent<SkeletonAnimation>();
        }

        private void OnComplete(TrackEntry trackentry)
        {
            Log.Info($"{GetType()} OnComplete");
            _Event.OnComplete?.Invoke();
            _Event.Reset();
            _anim.AnimationState.GetCurrent(0).Complete -= OnComplete;
        }

        public void Play(EAnimState animState, bool loop = true, Action onComplete = null)
        {
            _Event.Reset();
            switch (animState)
            {
                case EAnimState.BalloonFlyingDown:
                case EAnimState.BalloonFlyingLoop:
                case EAnimState.BalloonFlyingUp:
                case EAnimState.BalloonIn:
                case EAnimState.BalloonOut:
                case EAnimState.DearIdol:
                case EAnimState.DearJump:
                case EAnimState.DearSmile:
                case EAnimState.EatJun:
                case EAnimState.EatNormal:
                case EAnimState.FriendHightouch:
                case EAnimState.ManaIdle:
                case EAnimState.ManaJump:
                case EAnimState.NoWeaponIdle:
                case EAnimState.NoWeaponJoyShort:
                case EAnimState.NoWeaponRun:
                case EAnimState.NoWeaponRunSuper:
                case EAnimState.RunHighJump:
                case EAnimState.RunJump:
                case EAnimState.RunSpringJump:
                case EAnimState.Smile:
                case EAnimState.StaminaKarinOjigi:
                case EAnimState.UnitRaceEatShaveice:
                case EAnimState.UnitRaceRunCarpet:
                case EAnimState.UnitRaceSurfing:
                case EAnimState.UnitRaceSurfingIn:
                case EAnimState.UnitRaceSurfingOut:
                case EAnimState.UnitRaceSurfingUp:
                    PlayUniversalAnim(animState, loop);
                    break;
                case EAnimState.Attack:
                case EAnimState.AttackSkipQuest:
                case EAnimState.Damage:
                case EAnimState.Die:
                case EAnimState.Idle:
                case EAnimState.JoyLong:
                case EAnimState.JoyLongReturn:
                case EAnimState.JoyShort:
                case EAnimState.JoyShortReturn:
                case EAnimState.Landing:
                case EAnimState.MultiIdleNoWeapon:
                case EAnimState.MultiIdleStandBy:
                case EAnimState.MultiStandBy:
                case EAnimState.Run:
                case EAnimState.RunGamestart:
                case EAnimState.StandBy:
                case EAnimState.Walk:
                    PlayNormalAnim(animState, loop);
                    break;
                case EAnimState.JoyResult:
                case EAnimState.Skill0:
                case EAnimState.Skill1:
                case EAnimState.Skill2:
                case EAnimState.SkillEvolution0:
                    PlaySkillAnim(animState, loop);
                    break;
            }

            _AnimState = animState;
            _Event.OnComplete = onComplete;
        }

        public void Pause()
        {
            SetAnimSpeed(0);
        }

        public void Continue()
        {
            SetAnimSpeed(1);
        }

        public void ResetAnim()
        {
            _Event.Reset();
        }

        public void SetAnimSpeed(float speed)
        {
            _anim.AnimationState.TimeScale = speed;
        }

        public Animation GetCurrentAnimation(float track)
        {
            var currentAnimation = _anim.AnimationState.GetCurrent(0);
            return currentAnimation.Animation;
        }

        private void PlayNormalAnim(EAnimState animState, bool loop = true)
        {
            if (_AnimState == animState) return;
            if (!_animNameDict.TryGetValue(animState, out string animName))
            {
                animName = CombineAnimName(_NormalPrefix, animState);
                _animNameDict[animState] = animName;
            }

            RealPlay(0, loop, animName);
        }


        private void PlaySkillAnim(EAnimState animState, bool loop = true)
        {
            if (_AnimState == animState) return;
            if (!_animNameDict.TryGetValue(animState, out string animName))
            {
                animName = CombineAnimName(_SkillPrefix, animState);
                _animNameDict[animState] = animName;
            }

            RealPlay(0, loop, animName);
        }

        private void PlayUniversalAnim(EAnimState animState, bool loop = true)
        {
            if (_AnimState == animState) return;
            if (!_animNameDict.TryGetValue(animState, out string animName))
            {
                animName = CombineAnimName("000000_", animState);
                _animNameDict[animState] = animName;
            }

            RealPlay(0, loop, animName);
        }

        private string CombineAnimName(string prefix, EAnimState animState)
        {
            return $"{prefix}_{S_AnimNameDict[animState]}";
        }

        private void RealPlay(int tracksIndex, bool loop, string animName)
        {
            _anim.AnimationState.SetAnimation(tracksIndex, animName, loop);
            _anim.AnimationState.GetCurrent(tracksIndex).Complete += OnComplete;
        }

        public EAnimState _TestState;

        [Button("TestPlay")]
        public void TestPlay()
        {
            float time = Time.time;
            Play(_TestState, true, () => Log.Debug($"Complete {Time.time - time}"));
        }
    }
}