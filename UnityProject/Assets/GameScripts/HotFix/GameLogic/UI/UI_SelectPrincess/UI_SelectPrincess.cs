using System;
using System.Collections.Generic;
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

        /// <summary>
        /// 点击开始
        /// </summary>
        private void OnClickBtn_StartBattleBtn()
        {
            Btn_StartBattle.gameObject.SetActiveSelf(false);
            Root_ToBeSelectedBar.gameObject.SetActiveSelf(false);

            Battle.Instance.SelectPrincessSystem.ConfirmPrincessCard();
        }

        /// <summary>
        /// 点击菜单
        /// </summary>
        private void OnClickBtn_MenuBtn()
        {
            GameModule.UI.ShowUI<UI_Menu>();
        }

        #endregion


        protected override void OnCreate()
        {
            base.OnCreate();
            Item_Select.SetActive(false);
            Item_ToBeSelected.SetActive(false);

            _selectedPrincessCardPool = GameModule.ObjectPool.CreateSingleSpawnObjectPool<UI_SelectedPrincessCard>();
            _optionalPrincessCardPool = GameModule.ObjectPool.CreateSingleSpawnObjectPool<UI_OptionalPrincessCard>();

            AddUIEvent(UIEvent.UpdateSelectedPrincess, OnUpdateSelectedPrincess);
            AddUIEvent(UIEvent.ResetSelectPrincess, () => Root_SelectBar.GetComponent<ToggleGroup>().SetAllTogglesOff());


            var princessDict = Battle.Instance._OptionalPrincessDict;
            foreach (var (key, value) in princessDict)
            {
                UI_OptionalPrincessCard optionalPrincessCard = Spawn<UI_OptionalPrincessCard>();
                optionalPrincessCard.Init(value.SelectedPrincessCardData, LoadIcon(value.PrincessType), value.isSelected);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameModule.ObjectPool.DestroyObjectPool<UI_SelectedPrincessCard>();
            GameModule.ObjectPool.DestroyObjectPool<UI_OptionalPrincessCard>();
        }

        private void OnUpdateSelectedPrincess()
        {
            var dict = Battle.Instance._OptionalPrincessDict;
            var list = Battle.Instance._LeftPrincessCardList;

            UpdateOptionalPrincessCard(dict);
            UpdateSelectedPrincessCard(list);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            switch (Battle.Instance.BattleType)
            {
                case EBattleType.SelectPrincessCard:
                    break;
                case EBattleType.Battle:

                    List<SelectedPrincessCardData> princessCardList = Battle.Instance._LeftPrincessCardList;

                    for (int i = 0; i < princessCardList.Count; i++)
                    {
                        SelectedPrincessCardData selectedPrincessCardData = princessCardList[i];
                        UI_SelectedPrincessCard selectedPrincessCard = _selectedUIPrincessCardList[i];

                        if (selectedPrincessCardData.PrincessType == selectedPrincessCard._PrincessType)
                        {
                            selectedPrincessCard.Init(selectedPrincessCardData, LoadIcon(selectedPrincessCardData.PrincessType));
                        }
                    }


                    Toggle activeToggle = Root_SelectBar.GetComponent<ToggleGroup>().GetFirstActiveToggle();
                    if (activeToggle == null)
                    {
                        Battle.Instance.PlantSystem.SelectedPrincessType.Value = EPrincessType.Null;
                    }
                    else
                    {
                        Battle.Instance.PlantSystem.SelectedPrincessType.Value = Enum.Parse<EPrincessType>(activeToggle.name);
                    }

                    break;
            }
        }


        private Dictionary<EPrincessType, SelectPrincessData> _dict = new Dictionary<EPrincessType, SelectPrincessData>();

        public class SelectPrincessData
        {
            public GameObject ToBeSelected;
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
                if (_selectedPrincessCardPool.CanSpawn())
                {
                    ret = _selectedPrincessCardPool.Spawn() as T;
                }
                else
                {
                    GameObject target = Object.Instantiate(Item_Select, Root_SelectBar);
                    ret = UI_SelectedPrincessCard.CreateInstance(target) as T;
                    _selectedPrincessCardPool.Register(ret as UI_SelectedPrincessCard, true);
                }

                return ret;
            }

            if (typeof(T) == typeof(UI_OptionalPrincessCard))
            {
                if (_optionalPrincessCardPool.CanSpawn())
                {
                    ret = _optionalPrincessCardPool.Spawn() as T;
                }
                else
                {
                    GameObject target = Object.Instantiate(Item_ToBeSelected, Root_ToBeSelectedBar);
                    ret = UI_OptionalPrincessCard.CreateInstance(target) as T;
                    _optionalPrincessCardPool.Register(ret as UI_OptionalPrincessCard, true);
                }

                return ret;
            }

            return null;
        }
    }
}