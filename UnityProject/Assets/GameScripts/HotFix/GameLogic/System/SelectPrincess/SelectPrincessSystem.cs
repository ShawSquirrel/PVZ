using System.Collections.Generic;
using System.Linq;
using GameConfig;
using TEngine;

namespace GameLogic
{
    public class SelectPrincessSystem : ASystem
    {
        public void Init()
        {
            Dictionary<EPrincessType, OptionalPrincessCardData> toBeSelectedPrincessDict = Battle.Instance._OptionalPrincessDict;
            toBeSelectedPrincessDict.Clear();
            OptionalPrincessCardData optionalPrincessCardData;

            optionalPrincessCardData = MemoryPool.Acquire<OptionalPrincessCardData>();
            optionalPrincessCardData.Init(EPrincessType.CaoYeYouYi);
            toBeSelectedPrincessDict.Add(EPrincessType.CaoYeYouYi, optionalPrincessCardData);

            optionalPrincessCardData = MemoryPool.Acquire<OptionalPrincessCardData>();
            optionalPrincessCardData.Init(EPrincessType.PeiKeLiMu);
            toBeSelectedPrincessDict.Add(EPrincessType.PeiKeLiMu, optionalPrincessCardData);
        }

        protected override void Awake()
        {
            base.Awake();
            AddListener();
        }

        private void AddListener()
        {
        }

        public void ConfirmPrincessCard()
        {
            Battle.Instance.BattleType = EBattleType.Battle;
        }

        public void Selected(EPrincessType selectedPrincessType)
        {
            Dictionary<EPrincessType, OptionalPrincessCardData> toBeSelectedPrincessDict = Battle.Instance._OptionalPrincessDict;
            List<SelectedPrincessCardData> selectedPrincessList = Battle.Instance._LeftPrincessCardList;

            if (selectedPrincessList.Any(princessCard => princessCard.PrincessType == selectedPrincessType))
            {
                Log.Error("SelectPrincessSystem :: Selected");
                return;
            }

            SelectedPrincessCardData cardData = MemoryPool.Acquire<SelectedPrincessCardData>();
            cardData.Init(selectedPrincessType);

            selectedPrincessList.Add(cardData);
            toBeSelectedPrincessDict[selectedPrincessType].isSelected = true;

            GameEvent.Send(UIEvent.UpdateSelectedPrincess);
        }

        public void UnSelected(EPrincessType selectedPrincessType)
        {
            Dictionary<EPrincessType, OptionalPrincessCardData> toBeSelectedPrincessDict = Battle.Instance._OptionalPrincessDict;
            List<SelectedPrincessCardData> selectedPrincessList = Battle.Instance._LeftPrincessCardList;

            bool isContains = false;

            foreach (SelectedPrincessCardData princessCard in selectedPrincessList)
            {
                if (princessCard.PrincessType == selectedPrincessType)
                {
                    isContains = true;

                    selectedPrincessList.Remove(princessCard);
                    MemoryPool.Release(princessCard);
                    toBeSelectedPrincessDict[selectedPrincessType].isSelected = false;

                    GameEvent.Send(UIEvent.UpdateSelectedPrincess);

                    break;
                }
            }

            if (isContains == false)
            {
                Log.Error("SelectPrincessSystem :: UnSelected");
                return;
            }
        }
    }
}