using System.Collections.Generic;
using GameConfig;
using TEngine;

namespace GameLogic
{
    public class SelectPrincessSystem : ASystem
    {
        public void Init()
        {
            Dictionary<EPrincessType, bool> toBeSelectedPrincessDict = Battle.Instance._ToBeSelectedPrincessDict;
            toBeSelectedPrincessDict.Clear();
            toBeSelectedPrincessDict.Add(EPrincessType.CaoYeYouYi, false);
            toBeSelectedPrincessDict.Add(EPrincessType.PeiKeLiMu, false);
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

            var config = ConfigSystem.Instance.Tables.TPrincessCard;
            List<PrincessCard> _princessCardList = Battle.Instance._PrincessCardList;
            List<EPrincessType> list = Battle.Instance._SelectedPrincessList;
            _princessCardList.Clear();
            foreach (EPrincessType princessType in list)
            {
                _princessCardList.Add(PrincessCard.ToObject(config.Get(princessType)));
            }
        }

        public void Selected(EPrincessType selectedPrincessType)
        {
            Dictionary<EPrincessType, bool> toBeSelectedPrincessDict = Battle.Instance._ToBeSelectedPrincessDict;
            List<EPrincessType> selectedPrincessList = Battle.Instance._SelectedPrincessList;

            if (selectedPrincessList.Contains(selectedPrincessType) == true)
            {
                Log.Error("SelectPrincessSystem :: Selected");
                return;
            }

            selectedPrincessList.Add(selectedPrincessType);
            toBeSelectedPrincessDict[selectedPrincessType] = true;
            GameEvent.Send(UIEvent.UpdateSelectedPrincess);
        }

        public void UnSelected(EPrincessType selectedPrincessType)
        {
            Dictionary<EPrincessType, bool> toBeSelectedPrincessDict = Battle.Instance._ToBeSelectedPrincessDict;
            List<EPrincessType> selectedPrincessList = Battle.Instance._SelectedPrincessList;

            if (selectedPrincessList.Contains(selectedPrincessType) == false)
            {
                Log.Error("SelectPrincessSystem :: UnSelected");
                return;
            }

            selectedPrincessList.Remove(selectedPrincessType);
            toBeSelectedPrincessDict[selectedPrincessType] = false;
            GameEvent.Send(UIEvent.UpdateSelectedPrincess);
        }
    }
}