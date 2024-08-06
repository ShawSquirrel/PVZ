using TEngine;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public partial class UI_SelectLevel
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            int levelCount = GameApp.Instance.GameData.AdventureMaxLevel;
            Item_Btn.SetActiveSelf(false);
            for (int i = 0; i < levelCount; i++)
            {
                GameObject item = Object.Instantiate(Item_Btn, Item_Btn.transform.parent);
                item.SetActiveSelf(true);
                item.name = $"Level {i}";
                item.GetComponentInChildren<Text>().text = item.name;

                int k = i;
                item.GetComponent<Button>().onClick.AddListener(() => OnLevelButtonClick(k));
            }
        }

        
        private void OnLevelButtonClick(int i)
        {
            GameApp.Instance.LoadLevel(i);
        }
    }
}