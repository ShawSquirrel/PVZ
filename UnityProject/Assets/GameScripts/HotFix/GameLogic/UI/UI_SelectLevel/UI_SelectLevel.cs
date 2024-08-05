using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    partial class UI_SelectLevel : UIWindow
    {
        #region 脚本工具生成的代码
        private Button Btn_Exit;
        private GameObject Item_Btn;
        protected override void ScriptGenerator()
        {
            Btn_Exit = FindChildComponent<Button>("Panel/TopBar/Btn_Exit");
            Item_Btn = FindChild("Panel/Center/Item_Btn").gameObject;
            Btn_Exit.onClick.AddListener(UniTask.UnityAction(OnClickBtn_ExitBtn));
        }
        #endregion

        #region 事件
        private async UniTaskVoid OnClickBtn_ExitBtn()
        {
            Close();
            await UniTask.Yield();
        }
        #endregion

    }
}