using System.Collections.Generic;
using GameConfig;
using TEngine;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public partial class UI_SelectPrincess
    {
        public List<UI_SelectedPrincessCard> SelectedPrincessCardList = new List<UI_SelectedPrincessCard>();
        public IObjectPool<UI_SelectedPrincessCard> SelectedPrincessCardPool;

        private void UpdateSelectPrincessCard(List<EPrincessType> list)
        {
            if (SelectedPrincessCardList.Count < list.Count)
            {
                for (int i = 0; i < list.Count - SelectedPrincessCardList.Count; i++)
                {
                    SelectedPrincessCardList.Add(Spawn<UI_SelectedPrincessCard>());
                }
            }
            else
            {
                for (int i = SelectedPrincessCardList.Count - 1; i >= list.Count; i--)
                {
                    SelectedPrincessCardPool.Unspawn(SelectedPrincessCardList[i]);
                    SelectedPrincessCardList.Remove(SelectedPrincessCardList[i]);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                UI_SelectedPrincessCard card = SelectedPrincessCardList[i];
                card.Init(list[i], _dict);
            }
        }

        public class UI_SelectedPrincessCard : ObjectBase
        {
            private Toggle _toggle;
            private Button _button;

            protected override void EndObjectInitialize()
            {
                base.EndObjectInitialize();
                _toggle = (Target as GameObject).GetComponentInChildren<Toggle>();
                _button = (Target as GameObject).GetComponentInChildren<Button>();
            }

            public void Init(EPrincessType princessType, Dictionary<EPrincessType, SelectPrincessData> selectPrincessDatas)
            {
                Toggle toggle = _toggle;
                Button button = _button;


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

            protected override void OnSpawn()
            {
                base.OnSpawn();
                (Target as GameObject).SetActiveSelf(true);
            }

            protected override void OnUnSpawn()
            {
                base.OnUnSpawn();
                (Target as GameObject).SetActiveSelf(false);
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