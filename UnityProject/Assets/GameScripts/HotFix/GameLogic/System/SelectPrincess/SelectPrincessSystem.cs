using System.Collections.Generic;
using GameConfig;
using TEngine;

namespace GameLogic
{
    public class SelectPrincessSystem
    {
        public Dictionary<EPrincessType, bool> ToBeSelectedPrincessDict = new Dictionary<EPrincessType, bool>();
        public List<EPrincessType> SelectedPrincessList = new List<EPrincessType>();

        public void Init()
        {
            ToBeSelectedPrincessDict.Clear();
            ToBeSelectedPrincessDict.Add(EPrincessType.CaoYeYouYi, false);
            ToBeSelectedPrincessDict.Add(EPrincessType.PeiKeLiMu, false);
        }


        public void Selected(EPrincessType selectedPrincessType)
        {
            if (SelectedPrincessList.Contains(selectedPrincessType) == true)
            {
                Log.Error("SelectPrincessSystem :: Selected");
                return;
            }

            SelectedPrincessList.Add(selectedPrincessType);
            ToBeSelectedPrincessDict[selectedPrincessType] = true;
            GameEvent.Send(UIEvent.UpdateSelectedPrincess);
        }

        public void UnSelected(EPrincessType selectedPrincessType)
        {
            if (SelectedPrincessList.Contains(selectedPrincessType) == false)
            {
                Log.Error("SelectPrincessSystem :: UnSelected");
                return;
            }

            SelectedPrincessList.Remove(selectedPrincessType);
            ToBeSelectedPrincessDict[selectedPrincessType] = false;
            GameEvent.Send(UIEvent.UpdateSelectedPrincess);
        }
    }
}