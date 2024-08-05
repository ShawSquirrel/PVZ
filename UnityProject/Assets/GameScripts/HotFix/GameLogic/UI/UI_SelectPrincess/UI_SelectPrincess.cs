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
    public partial class UI_SelectPrincess : UIWindow
    {
        #region 脚本工具生成的代码
        private Transform Root_SelectBar;
        private GameObject Item_Select;
        private Transform Root_ToBeSelectedBar;
        private GameObject Item_ToBeSelected;
        private Button Btn_Menu;
        private Button Btn_StartBattle;
        protected override void ScriptGenerator()
        {
            Root_SelectBar = FindChild("Panel/Left/Root_SelectBar");
            Item_Select = FindChild("Panel/Left/Root_SelectBar/Item_Select").gameObject;
            Root_ToBeSelectedBar = FindChild("Panel/Right/Root_ToBeSelectedBar");
            Item_ToBeSelected = FindChild("Panel/Right/Root_ToBeSelectedBar/Item_ToBeSelected").gameObject;
            Btn_Menu = FindChildComponent<Button>("Panel/Right/Btn_Menu");
            Btn_StartBattle = FindChildComponent<Button>("Panel/Right/Btn_StartBattle");
            Btn_Menu.onClick.AddListener(OnClickBtn_MenuBtn);
            Btn_StartBattle.onClick.AddListener(OnClickBtn_StartBattleBtn);
        }
        #endregion

        #region 事件

        #endregion

        private void OnClickBtn_StartBattleBtn()
        {
            Btn_StartBattle.gameObject.SetActiveSelf(false);
            Battle.Instance.BattleType = EBattleType.Battle;
            Root_ToBeSelectedBar.gameObject.SetActiveSelf(false);
        }

        private void OnClickBtn_MenuBtn()
        {
            GameModule.UI.ShowUI<UI_Menu>();
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Item_Select.SetActive(false);
            Item_ToBeSelected.SetActive(false);

            SelectedPrincessCardPool = GameModule.ObjectPool.CreateSingleSpawnObjectPool<UI_SelectedPrincessCard>();

            AddUIEvent(UIEvent.UpdateSelectedPrincess, OnUpdateSelectedPrincess);
            AddUIEvent(UIEvent.ResetSelectPrincess, () => Root_SelectBar.GetComponent<ToggleGroup>().SetAllTogglesOff());


            var princessDict = Battle.Instance.SelectPrincessSystem.ToBeSelectedPrincessDict;
            foreach (var (key, value) in princessDict)
            {
                GenerateSelectPrincessToggle(key, value);
            }
        }

        private void OnUpdateSelectedPrincess()
        {
            var dict = Battle.Instance.SelectPrincessSystem.ToBeSelectedPrincessDict;
            var list = Battle.Instance.SelectPrincessSystem.SelectedPrincessList;

            foreach (var (key, value) in dict)
            {
                if (_dict.TryGetValue(key, out SelectPrincessData data) == false)
                {
                    Log.Error("UI_SelectPrincess :: OnUpdateSelectedPrincess");
                    return;
                }

                data.ToBeSelected.GetComponentInChildren<Toggle>().isOn = value;
            }

            UpdateSelectPrincessCard(list);
        }


        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (Battle.Instance.BattleType != EBattleType.Battle) return;

            Toggle activeToggle = Root_SelectBar.GetComponent<ToggleGroup>().GetFirstActiveToggle();
            if (activeToggle == null)
            {
                Battle.Instance.PlantSystem.SelectedPrincessType.Value = EPrincessType.Null;
            }
            else
            {
                Battle.Instance.PlantSystem.SelectedPrincessType.Value = Enum.Parse<EPrincessType>(activeToggle.name);
            }
        }


        private void GenerateSelectPrincessToggle(EPrincessType princessType, bool isOn)
        {
            GameObject toggleObj = Object.Instantiate(Item_ToBeSelected, Root_ToBeSelectedBar);

            if (_dict.TryGetValue(princessType, out SelectPrincessData data) == false)
            {
                data = new SelectPrincessData();
                _dict.Add(princessType, data);
            }

            data.ToBeSelected ??= toggleObj;

            toggleObj.name = princessType.ToString();
            toggleObj.SetActive(true);

            Toggle toggle = toggleObj.GetComponentInChildren<Toggle>();
            Button button = toggleObj.GetComponentInChildren<Button>();
            toggle.isOn = isOn;
            toggle.interactable = false;
            toggle.targetGraphic.GetComponent<Image>().sprite = LoadIcon(princessType);
            toggle.graphic.GetComponent<Image>().sprite = LoadIcon(princessType);

            button.onClick.AddListener(OnButtonClick);

            void OnButtonClick()
            {
                if (toggle.isOn)
                {
                    Battle.Instance.SelectPrincessSystem.UnSelected(princessType);
                }
                else
                {
                    Battle.Instance.SelectPrincessSystem.Selected(princessType);
                }
            }
        }

        private Dictionary<EPrincessType, SelectPrincessData> _dict = new Dictionary<EPrincessType, SelectPrincessData>();

        public class SelectPrincessData
        {
            public GameObject ToBeSelected;
            public GameObject Selected;
            public Sprite Icon;
        }


        private Sprite LoadIcon(EPrincessType princessType)
        {
            if (_dict.TryGetValue(princessType, out SelectPrincessData data) == false)
            {
                data = new SelectPrincessData();
                _dict.Add(princessType, data);
            }

            data.Icon ??= GameModule.Resource.LoadAsset<Sprite>($"Icon_{princessType}");

            return data.Icon;
        }


        private T Spawn<T>() where T : ObjectBase
        {
            T ret;
            if (typeof(T) == typeof(UI_SelectedPrincessCard))
            {
                if (SelectedPrincessCardPool.CanSpawn())
                {
                    ret = SelectedPrincessCardPool.Spawn() as T;
                }
                else
                {
                    GameObject target = Object.Instantiate(Item_Select, Root_SelectBar);
                    ret = UI_SelectedPrincessCard.CreateInstance(target) as T;
                    SelectedPrincessCardPool.Register(ret as UI_SelectedPrincessCard, true);
                }

                return ret;
            }

            return null;
        }
    }
}