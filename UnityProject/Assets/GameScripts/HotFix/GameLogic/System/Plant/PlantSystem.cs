using System;
using System.Collections.Generic;
using GameConfig;
using TEngine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameLogic
{

    public class PlantSystem : ASystem
    {
        public bool CanPlant => SelectedPrincessType.Value != EPrincessType.Null;
        public BindValue<EPrincessType> SelectedPrincessType = new BindValue<EPrincessType>();
        private APrincess _selectPrincess;

        public void Init()
        {
            SelectedPrincessType.AddListener(OnSelectedPrincessTypeChanged);
        }


        private void OnSelectedPrincessTypeChanged(EPrincessType lastPrincessType, EPrincessType currentPrincessType)
        {
            if (_selectPrincess != null)
            {
                switch (lastPrincessType)
                {
                    case EPrincessType.CaoYeYouYi:
                        PoolHelper.UnSpawn(_selectPrincess as Princess_CaoYeYouYi);
                        break;
                    case EPrincessType.PeiKeLiMu:
                        PoolHelper.UnSpawn(_selectPrincess as Princess_PeiKeLiMu);
                        break;
                }
            }

            _selectPrincess = null;

            switch (currentPrincessType)
            {
                case EPrincessType.CaoYeYouYi:
                    _selectPrincess = PoolHelper.Spawn<Princess_CaoYeYouYi>();
                    break;
                case EPrincessType.PeiKeLiMu:
                    _selectPrincess = PoolHelper.Spawn<Princess_PeiKeLiMu>();
                    break;
            }
        }

        public void Plant(Vector2Int mapItemIndex)
        {
            MapData mapData = Battle.Instance.MapSystem._mapDataDict[mapItemIndex];

            if (mapData._MapItem.Planted() == false) return;
            if (_selectPrincess.Plant(mapData))
            {
                mapData._Princess = _selectPrincess;
                mapData._Princess.PlantCallBack(mapData);
                Log.Info($"{mapData._Princess._TF.name} Plant to {mapData._MapItem._TF.name}");
                _selectPrincess = null;
                GameEvent.Send(UIEvent.ResetSelectPrincess);

                Battle.Instance.CoolDownSystem.PlantedPrincessCard(mapData._Princess.PrincessType);
            }
        }


        protected override void Update()
        {
            if (_selectPrincess != null)
            {
                _selectPrincess._TF.position = MouseHelper.GetMousePointToWorldPoint();
            }
        }
    }
}