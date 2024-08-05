using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class UI_Menu : UIWindow
    {
        #region 脚本工具生成的代码
        private Button Btn_Exit;
        private Button Btn_Resume;
        private Button Btn_Restart;
        protected override void ScriptGenerator()
        {
            Btn_Exit = FindChildComponent<Button>("Panel/Btn_Exit");
            Btn_Resume = FindChildComponent<Button>("Panel/Btn_Resume");
            Btn_Restart = FindChildComponent<Button>("Panel/Btn_Restart");
            Btn_Exit.onClick.AddListener(UniTask.UnityAction(OnClickBtn_ExitBtn));
            Btn_Resume.onClick.AddListener(UniTask.UnityAction(OnClickBtn_ResumeBtn));
            Btn_Restart.onClick.AddListener(UniTask.UnityAction(OnClickBtn_RestartBtn));
        }
        #endregion

        #region 事件
        private async UniTaskVoid OnClickBtn_ExitBtn()
        {
            Close();
            await UniTask.Yield();
        }
        private async UniTaskVoid OnClickBtn_ResumeBtn()
        {
            Close();
            await UniTask.Yield();
        }
        private async UniTaskVoid OnClickBtn_RestartBtn()
        {
            Close();
            await UniTask.Yield();
        }
        #endregion

    }
}