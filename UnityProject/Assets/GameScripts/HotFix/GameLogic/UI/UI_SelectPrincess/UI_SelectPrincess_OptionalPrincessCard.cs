using System.Collections.Generic;
using GameConfig;
using TEngine;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public partial class UI_SelectPrincess
    {
        private List<UI_OptionalPrincessCard> _optionalPrincessCardList = new List<UI_OptionalPrincessCard>();
        private IObjectPool<UI_OptionalPrincessCard> _optionalPrincessCardPool;

        private void UpdateOptionalPrincessCard(Dictionary<EPrincessType, OptionalPrincessCardData> dict)
        {
            foreach (UI_OptionalPrincessCard uiOptionalPrincessCard in _optionalPrincessCardList)
            {
                _optionalPrincessCardPool.Unspawn(uiOptionalPrincessCard);
            }

            _optionalPrincessCardList.Clear();

            foreach (var (key, value) in dict)
            {
                UI_OptionalPrincessCard optionalPrincessCard = Spawn<UI_OptionalPrincessCard>();
                optionalPrincessCard.Init(value.SelectedPrincessCardData, LoadIcon(key), value.isSelected);
            }
        }

        public class UI_OptionalPrincessCard : ObjectBase, IUnit
        {
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

            public void Init(SelectedPrincessCardData selectedPrincessCardData, Sprite icon, bool isOn)
            {
                Toggle toggle = _toggle;
                Button button = _button;

                toggle.name = selectedPrincessCardData.PrincessType.ToString();
                toggle.targetGraphic.GetComponent<Image>().sprite = icon;
                toggle.isOn = isOn;

                button.image.sprite = icon;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnSelectedPrincessToggleClick(selectedPrincessCardData.PrincessType));

                _TF.Find("Cost").GetComponentInChildren<Text>().text = selectedPrincessCardData.Cost.ToString();

                void OnSelectedPrincessToggleClick(EPrincessType princessType)
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

            public static UI_OptionalPrincessCard CreateInstance(GameObject target)
            {
                UI_OptionalPrincessCard card = MemoryPool.Acquire<UI_OptionalPrincessCard>();
                card.Initialize_Out(target);

                return card;
            }
        }
    }
}