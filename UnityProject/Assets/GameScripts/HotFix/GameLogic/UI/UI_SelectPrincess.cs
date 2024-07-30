using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class UI_SelectPrincess : UIWindow
    {
        #region 脚本工具生成的代码
        private Toggle Toggle_CaoYeYouYi;
        protected override void ScriptGenerator()
        {
            Toggle_CaoYeYouYi = FindChildComponent<Toggle>("Panel/Bar/Toggle_CaoYeYouYi");
            Toggle_CaoYeYouYi.onValueChanged.AddListener((isOn) => OnToggleValueChange(isOn, Toggle_CaoYeYouYi.name));
        }
        #endregion

        #region 事件
        private void OnToggleValueChange(bool isOn, string name)
        {
            if (isOn)
            {
                GameApp.Instance.SelectPrincess(EPrincessType.CaoYeYouYi);
            }
        }
        #endregion

    }
}