using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.Top)]
    partial class UI_LoadSplash : UIWindow
    {
        #region 脚本工具生成的代码
        private Image Img_Background;
        protected override void ScriptGenerator()
        {
            Img_Background = FindChildComponent<Image>("Img_Background");
        }
        #endregion

        #region 事件
        #endregion

    }
}