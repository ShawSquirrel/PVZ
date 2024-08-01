using System;
using TEngine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameLogic
{
    public class PlantSystem : IPlantSystem
    {
        public bool CanPlant => SelectedPrincessType.Value != EPrincessType.Null;

        public BindValue<EPrincessType> SelectedPrincessType = new BindValue<EPrincessType>();
        private APrincess SelectPrincess;

        public void Init()
        {
            SelectedPrincessType.AddListener(OnSelectedPrincessTypeChanged);
            Utility.Unity.AddUpdateListener(OnUpdate);
        }


        private void OnSelectedPrincessTypeChanged(EPrincessType lastPrincessType, EPrincessType currentPrincessType)
        {
            if (currentPrincessType == EPrincessType.Null)
            {
                if (SelectPrincess != null)
                {
                    switch (lastPrincessType)
                    {
                        case EPrincessType.CaoYeYouYi:
                            PoolHelper.UnSpawn(SelectPrincess as Princess_CaoYeYouYi);
                            break;
                        case EPrincessType.PeiKeLiMu:
                            PoolHelper.UnSpawn(SelectPrincess as Princess_CaoYeYouYi);
                            break;
                    }
                    
                }

                SelectPrincess = null;
                return;
            }
            switch (currentPrincessType)
            {
                case EPrincessType.CaoYeYouYi:
                    SelectPrincess = PoolHelper.Spawn<Princess_CaoYeYouYi>();
                    break;
                case EPrincessType.PeiKeLiMu:
                    SelectPrincess = PoolHelper.Spawn<Princess_CaoYeYouYi>();
                    break;
            }
        }

        public void Plant(Vector2Int mapItemIndex)
        {
            MapData mapData = Battle.Instance.MapSystem.MapDataDict[mapItemIndex];
            if (mapData.Princess == null && mapData.MapItem is ICanPlanted canPlanted)
            {
                mapData.Princess = SelectPrincess;

                SelectPrincess.TF.position = mapData.MapItem.TF.position;
                SelectPrincess = null;
                SelectedPrincessType.Value = EPrincessType.Null;
                GameEvent.Send(UIEvent.ResetSelectPrincess);
                
            }
        }

        private void OnUpdate()
        {
            if (SelectPrincess != null)
            {
                SelectPrincess.TF.position = MouseHelper.GetMouseToWorld();
            }
        }
    }
}