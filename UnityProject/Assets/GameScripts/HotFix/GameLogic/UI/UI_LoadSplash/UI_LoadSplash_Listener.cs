using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

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
    }
}