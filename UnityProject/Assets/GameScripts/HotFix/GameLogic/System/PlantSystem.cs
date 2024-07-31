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
        private GameObject SelectPrincessPrefab;

        public void Init()
        {
            SelectedPrincessType.AddListener(OnSelectedPrincessTypeChanged);
            Utility.Unity.AddUpdateListener(OnUpdate);
        }


        private void OnSelectedPrincessTypeChanged(EPrincessType princessType)
        {
            if (princessType == EPrincessType.Null)
            {
                if (SelectPrincessPrefab != null)
                {
                    Object.Destroy(SelectPrincessPrefab);
                }

                SelectPrincessPrefab = null;
                return;
            }

            SelectPrincessPrefab = Object.Instantiate(GameModule.Resource.LoadAsset<GameObject>($"Princess_{princessType}"));
        }

        public void Plant(Vector2Int mapItemIndex)
        {
            MapData mapData = Battle.Instance.MapSystem.MapDataDict[mapItemIndex];
            if (mapData.Princess == null && mapData.MapItem is ICanPlanted canPlanted)
            {
                mapData.Princess                        = new Princess_CaoYeYouYi();
                SelectPrincessPrefab.transform.position = mapData.MapItem.TF.position;

                SelectPrincessPrefab = null;
                SelectedPrincessType.Value = EPrincessType.Null;
                GameEvent.Send(UIEvent.ResetSelectPrincess);
            }
        }

        private void OnUpdate()
        {
            if (SelectPrincessPrefab != null)
            {
                SelectPrincessPrefab.transform.position = MouseHelper.GetMouseToWorld();
            }
        }
    }
}