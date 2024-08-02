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
        private AActor _selectActor;

        public void Init()
        {
            SelectedPrincessType.AddListener(OnSelectedPrincessTypeChanged);
            Utility.Unity.AddUpdateListener(OnUpdate);
        }


        private void OnSelectedPrincessTypeChanged(EPrincessType lastPrincessType, EPrincessType currentPrincessType)
        {
            if (_selectActor != null)
            {
                switch (lastPrincessType)
                {
                    case EPrincessType.CaoYeYouYi:
                        PoolHelper.UnSpawn(_selectActor as ActorCaoYeYouYi);
                        break;
                    case EPrincessType.PeiKeLiMu:
                        PoolHelper.UnSpawn(_selectActor as ActorPeiKeLiMu);
                        break;
                }
            }

            _selectActor = null;
            
            switch (currentPrincessType)
            {
                case EPrincessType.CaoYeYouYi:
                    _selectActor = PoolHelper.Spawn<ActorCaoYeYouYi>();
                    break;
                case EPrincessType.PeiKeLiMu:
                    _selectActor = PoolHelper.Spawn<ActorPeiKeLiMu>();
                    break;
            }
        }

        public void Plant(Vector2Int mapItemIndex)
        {
            MapData mapData = Battle.Instance.MapSystem._mapDataDict[mapItemIndex];
            AMapItem mapItem = mapData._MapItem;

            if (mapData._Actor != null) return;
            if (mapItem.Planted() == false) return;
            if (_selectActor.Plant(mapItem))
            {
                mapData._Actor = _selectActor;
                Log.Info($"{mapData._Actor._TF.name} Plant to {mapData._MapItem._TF.name}");
                _selectActor = null;
                GameEvent.Send(UIEvent.ResetSelectPrincess);
            }
        }

        private void OnUpdate()
        {
            if (_selectActor != null)
            {
                _selectActor._TF.position = MouseHelper.GetMousePointToWorldPoint();
            }
        }
    }
}