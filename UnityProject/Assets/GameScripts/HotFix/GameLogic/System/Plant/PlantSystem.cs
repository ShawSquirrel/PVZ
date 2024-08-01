using System;
using GameConfig;
using TEngine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameLogic
{
    public class PlantSystem : IPlantSystem
    {
        public bool CanPlant => SelectedPrincessType.Value != EPrincessType.Null;

        public BindValue<EPrincessType> SelectedPrincessType = new BindValue<EPrincessType>();
        private APrincess _SelectPrincess;

        public void Init()
        {
            SelectedPrincessType.AddListener(OnSelectedPrincessTypeChanged);
            Utility.Unity.AddUpdateListener(OnUpdate);
        }


        private void OnSelectedPrincessTypeChanged(EPrincessType lastPrincessType, EPrincessType currentPrincessType)
        {
            if (_SelectPrincess != null)
            {
                switch (lastPrincessType)
                {
                    case EPrincessType.CaoYeYouYi:
                        PoolHelper.UnSpawn(_SelectPrincess as Princess_CaoYeYouYi);
                        break;
                    case EPrincessType.PeiKeLiMu:
                        PoolHelper.UnSpawn(_SelectPrincess as Princess_PeiKeLiMu);
                        break;
                }
            }

            _SelectPrincess = null;
            
            switch (currentPrincessType)
            {
                case EPrincessType.CaoYeYouYi:
                    _SelectPrincess = PoolHelper.Spawn<Princess_CaoYeYouYi>();
                    break;
                case EPrincessType.PeiKeLiMu:
                    _SelectPrincess = PoolHelper.Spawn<Princess_PeiKeLiMu>();
                    break;
            }
        }

        public void Plant(Vector2Int mapItemIndex)
        {
            MapData mapData = Battle.Instance.MapSystem._MapDataDict[mapItemIndex];
            AMapItem mapItem = mapData._MapItem;

            if (mapData._Princess != null) return;
            if (mapItem.Planted() == false) return;
            if (_SelectPrincess.Plant(mapItem))
            {
                mapData._Princess = _SelectPrincess;
                Log.Info($"{mapData._Princess._TF.name} Plant to {mapData._MapItem._TF.name}");
                _SelectPrincess = null;
                GameEvent.Send(UIEvent.ResetSelectPrincess);
            }
        }

        private void OnUpdate()
        {
            if (_SelectPrincess != null)
            {
                _SelectPrincess._TF.position = MouseHelper.GetMousePointToWorldPoint();
            }
        }
    }
}