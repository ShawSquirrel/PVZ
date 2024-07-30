using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class UI_Main : UIWindow
    {
        #region 脚本工具生成的代码

        private Dropdown Dropdown_SelectLocalization;
        private Button Btn_Start;

        protected override void ScriptGenerator()
        {
            Dropdown_SelectLocalization = FindChildComponent<Dropdown>("Panel/Dropdown_SelectLocalization");
            Dropdown_SelectLocalization.onValueChanged.AddListener(OnValueChanged_SelectLocalizationDropdown);
            Btn_Start = FindChildComponent<Button>("Panel/Btn_Start");
            Btn_Start.onClick.AddListener(UniTask.UnityAction(OnClickBtn_StartBtn));
        }

        #endregion

        #region 事件

        private async UniTaskVoid OnClickBtn_StartBtn()
        {
            GameApp.Instance.StartGame();
            Close();
            await UniTask.Yield();
        }

        #endregion

        private void OnValueChanged_SelectLocalizationDropdown(int id)
        {
            switch (Dropdown_SelectLocalization.captionText.text)
            {
                case "简体中文":
                    GameModule.Localization.Language = Language.ChineseSimplified;
                    break;
                case "英语":
                    GameModule.Localization.Language = Language.English;
                    break;
                case "日语":
                    GameModule.Localization.Language = Language.Japanese;
                    break;
            }
        }
    }
}