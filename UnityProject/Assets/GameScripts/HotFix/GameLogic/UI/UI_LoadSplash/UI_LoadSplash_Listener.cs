using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public partial class UI_LoadSplash
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            AddUIEvent<float>(UIEvent.SetLoadSplash, OnSetLoadSplash);
        }

        private void OnSetLoadSplash(float alpha)
        {
            Color color = Img_Background.color;
            color.a = alpha;
            Img_Background.color = color;
        }

        public async UniTaskVoid Show()
        {
            bool isComplete = false;
            DOTween.ToAlpha(() => Img_Background.color, value => Img_Background.color = value, 1f, 1f)
               .OnComplete(() => isComplete = true);
            await UniTask.WaitUntil(() => isComplete);
        }

        public async UniTask Hide()
        {
            bool isComplete = false;
            DOTween.ToAlpha(() => Img_Background.color, value => Img_Background.color = value, 0f, 1f)
               .OnComplete(() => isComplete = true);
            await UniTask.WaitUntil(() => isComplete);
        }
    }
}