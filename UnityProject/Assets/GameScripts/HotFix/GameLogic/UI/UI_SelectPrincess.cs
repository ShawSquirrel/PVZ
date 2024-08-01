using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameConfig;
using UnityEngine;
using UnityEngine.UI;
using TEngine;
using Object = UnityEngine.Object;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class UI_SelectPrincess : UIWindow
    {
        #region 脚本工具生成的代码

        private Toggle Toggle_Item;

        protected override void ScriptGenerator()
        {
            Toggle_Item = FindChildComponent<Toggle>("Panel/Bar/Toggle_Item");
            // Toggle_Item.onValueChanged.AddListener(OnToggleToggle_ItemChange);
        }

        #endregion

        #region 事件

        #endregion

        protected override void OnCreate()
        {
            base.OnCreate();
            AddUIEvent(UIEvent.ResetSelectPrincess, () => Toggle_Item.group.SetAllTogglesOff());
            
            
            List<EPrincessType> princessList = UserData as List<EPrincessType>;
            foreach (EPrincessType princessType in princessList)
            {
                GameObject toggle = GeneratePrincessToggle(princessType);
            }
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            Toggle activeToggle = Toggle_Item.group.GetFirstActiveToggle();
            if (activeToggle == null)
            {
                Battle.Instance.PlantSystem.SelectedPrincessType.Value = EPrincessType.Null;
            }
            else
            {
                Battle.Instance.PlantSystem.SelectedPrincessType.Value = Enum.Parse<EPrincessType>(activeToggle.name);
            }
        }


        private GameObject GeneratePrincessToggle(EPrincessType princessType)
        {
            GameObject toggle = Object.Instantiate(Toggle_Item.gameObject, Toggle_Item.transform.parent);
            toggle.name = princessType.ToString();
            toggle.SetActive(true);
            // toggle.GetComponent<Toggle>().onValueChanged.AddListener((isOn) => OnToggleToggle_ItemChange(isOn, princessType));
            return toggle;
        }
    }
}