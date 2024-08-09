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

        public void ConfirmPrincessCard()
        {
            Battle.Instance.BattleType = EBattleType.Battle;
        }

        public void Selected(EPrincessType princessType)
        {
            Dictionary<EPrincessType, OptionalPrincessCardData> optionalPrincessDict = Battle.Instance._OptionalPrincessDict;
            List<SelectedPrincessCardData> selectedPrincessList = Battle.Instance._SelectedPrincessCardList;

            if (selectedPrincessList.Any(princessCard => princessCard.PrincessType == princessType))
            {
                Log.Error($"SelectPrincessSystem :: Selected :: {princessType}");
                return;
            }

            SelectedPrincessCardData cardData = MemoryPool.Acquire<SelectedPrincessCardData>();
            cardData.Init(princessType);

            selectedPrincessList.Add(cardData);
            optionalPrincessDict[princessType].isSelected = true;

            GameEvent.Send(UIEvent.UpdateSelectedPrincess);
        }

        public void UnSelected(EPrincessType princessType)
        {
            Dictionary<EPrincessType, OptionalPrincessCardData> toBeSelectedPrincessDict = Battle.Instance._OptionalPrincessDict;
            List<SelectedPrincessCardData> selectedPrincessList = Battle.Instance._SelectedPrincessCardList;

            bool isContains = false;

            foreach (SelectedPrincessCardData princessCard in selectedPrincessList)
            {
                if (princessCard.PrincessType == princessType)
                {
                    isContains = true;

                    selectedPrincessList.Remove(princessCard);
                    MemoryPool.Release(princessCard);
                    toBeSelectedPrincessDict[princessType].isSelected = false;

                    GameEvent.Send(UIEvent.UpdateSelectedPrincess);

                    break;
                }
            }

            if (isContains == false)
            {
                Log.Error($"SelectPrincessSystem :: UnSelected :: {princessType}");
                return;
            }
        }
    }
}