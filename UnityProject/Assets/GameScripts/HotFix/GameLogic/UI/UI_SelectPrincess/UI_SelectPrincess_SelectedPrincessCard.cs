using System.Collections.Generic;
using GameConfig;
using TEngine;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public partial class UI_SelectPrincess
    {
        private List<UI_SelectedPrincessCard> _selectedPrincessCardList = new List<UI_SelectedPrincessCard>();
        private IObjectPool<UI_SelectedPrincessCard> _selectedPrincessCardPool;

        private void UpdateSelectPrincessCard(List<EPrincessType> list)
        {
            if (_selectedPrincessCardList.Count < list.Count)
            {
                for (int i = 0; i < list.Count - _selectedPrincessCardList.Count; i++)
                {
                    _selectedPrincessCardList.Add(Spawn<UI_SelectedPrincessCard>());
                }
            }
            else
            {
                for (int i = _selectedPrincessCardList.Count - 1; i >= list.Count; i--)
                {
                    _selectedPrincessCardPool.Unspawn(_selectedPrincessCardList[i]);
                    _selectedPrincessCardList.Remove(_selectedPrincessCardList[i]);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                UI_SelectedPrincessCard card = _selectedPrincessCardList[i];
                card.Init(list[i], _dict);
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
                _Obj    = Target as GameObject;
                _TF     = _Obj.transform;
                _toggle = _Obj.GetComponentInChildren<Toggle>();
                _button = _Obj.GetComponentInChildren<Button>();
            }

            public void Init(EPrincessType princessType, Dictionary<EPrincessType, SelectPrincessData> selectPrincessDatas)
            {
                Toggle toggle = _toggle;
                Button button = _button;

                _PrincessType                                     = princessType;
                toggle.name                                       = princessType.ToString();
                toggle.targetGraphic.GetComponent<Image>().sprite = selectPrincessDatas[princessType].Icon;
                button.image.sprite                               = selectPrincessDatas[princessType].Icon;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnSelectedPrincessToggleClick(princessType));

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

            public void Init(PrincessCard princessCard)
            {
                if (princessCard.CoolDown == 0)
                {
                    _TF.Find("Mask").gameObject.SetActiveSelf(false);
                    _button.interactable = true;
                }
                else
                {
                    _TF.Find("Mask").gameObject.SetActiveSelf(true);
                    _TF.Find("Mask").GetComponent<Image>().fillAmount = princessCard.CoolDown / princessCard.MaxCoolDown;
                    
                    _button.interactable = false;
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