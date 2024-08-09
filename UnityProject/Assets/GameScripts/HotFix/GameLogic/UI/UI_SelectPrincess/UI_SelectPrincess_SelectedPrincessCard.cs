using System;
using System.Collections.Generic;
using GameConfig;
using TEngine;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace GameLogic
{
    public partial class UI_SelectPrincess
    {
        private List<UI_SelectedPrincessCard> _selectedUIPrincessCardList = new List<UI_SelectedPrincessCard>();
        private IObjectPool<UI_SelectedPrincessCard> _selectedPrincessCardPool;

        private void UpdateSelectedPrincessCard(List<SelectedPrincessCardData> list)
        {
            foreach (UI_SelectedPrincessCard uiPrincessCard in _selectedUIPrincessCardList)
            {
                _selectedPrincessCardPool.Unspawn(uiPrincessCard);
            }

            _selectedUIPrincessCardList.Clear();


            foreach (SelectedPrincessCardData princessCard in list)
            {
                UI_SelectedPrincessCard uiPrincessCard = Spawn<UI_SelectedPrincessCard>();
                uiPrincessCard.Init(princessCard, LoadIcon(princessCard.PrincessType));
                _selectedUIPrincessCardList.Add(uiPrincessCard);
            }
        }

        public class UI_SelectedPrincessCard : ObjectBase, IUnit
        {
            public EPrincessType _PrincessType;
            public GameObject _Obj { get; set; }
            public Transform _TF { get; set; }
            private Toggle _toggle;
            private Button _button;

            protected override void EndObjectInitialize()
            {
                base.EndObjectInitialize();
                _Obj = Target as GameObject;
                _TF = _Obj.transform;
                _toggle = _Obj.GetComponentInChildren<Toggle>();
                _button = _Obj.GetComponentInChildren<Button>();
            }

            public void Init(SelectedPrincessCardData selectedPrincessCardData, Sprite icon)
            {
                Toggle toggle = _toggle;
                Button button = _button;

                _PrincessType = selectedPrincessCardData.PrincessType;
                toggle.name = selectedPrincessCardData.PrincessType.ToString();
                toggle.targetGraphic.GetComponent<Image>().sprite = icon;
                button.image.sprite = icon;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnSelectedPrincessToggleClick(selectedPrincessCardData.PrincessType));

                _TF.Find("Cost").GetComponentInChildren<Text>().text = selectedPrincessCardData.Cost.ToString();

                switch (Battle.Instance.BattleType)
                {
                    case EBattleType.SelectPrincessCard:
                        _TF.Find("Mask").gameObject.SetActiveSelf(false);
                        _button.interactable = true;
                        break;
                    case EBattleType.Battle:
                        if (selectedPrincessCardData.CoolDown == 0)
                        {
                            _TF.Find("Mask").gameObject.SetActiveSelf(false);
                            _button.interactable = true;
                        }
                        else
                        {
                            _TF.Find("Mask").gameObject.SetActiveSelf(true);
                            _TF.Find("Mask").GetComponent<Image>().fillAmount = selectedPrincessCardData.CoolDown / selectedPrincessCardData.MaxCoolDown;
                            _button.interactable = false;
                        }

                        break;
                }


                void OnSelectedPrincessToggleClick(EPrincessType princessType)
                {
                    switch (Battle.Instance.BattleType)
                    {
                        case EBattleType.SelectPrincessCard:
                            Battle.Instance.SelectPrincessSystem.UnSelected(princessType);
                            break;
                        case EBattleType.Battle:
                            toggle.isOn = !toggle.isOn;
                            break;
                    }
                }
            }

            protected override void OnSpawn()
            {
                base.OnSpawn();
                _Obj.SetActiveSelf(true);
            }

            protected override void OnUnSpawn()
            {
                base.OnUnSpawn();
                _Obj.SetActiveSelf(false);
            }

            protected override void Release(bool isShutdown)
            {
                Object.Destroy(Target as GameObject);
            }

            public static UI_SelectedPrincessCard CreateInstance(GameObject target)
            {
                UI_SelectedPrincessCard card = MemoryPool.Acquire<UI_SelectedPrincessCard>();
                card.Initialize_Out(target);

                return card;
            }
        }
    }
}