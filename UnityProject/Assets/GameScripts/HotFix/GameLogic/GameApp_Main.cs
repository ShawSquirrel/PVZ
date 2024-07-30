using Cysharp.Threading.Tasks;
using GameLogic;
using TEngine;
using UnityEngine;

public partial class GameApp
{
    public EPrincessType SelectedPrincessType;
    private void AddLogicSystemMain()
    {
    }

    private async UniTask StartMain()
    {
        await GameInit();
        
        GameModule.UI.ShowUIAsync<UI_Main>();
        await UniTask.CompletedTask;
    }

    private async UniTask GameInit()
    {
        await GameModule.Localization.LoadLanguage("Localization");
        GameModule.Localization.Language = Language.ChineseSimplified;
        
        
        await UniTask.CompletedTask;
    }

    public void StartGame()
    {
        int[,] arr = new int[9, 6];
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                arr[i, j] = j % 2 == 0 ? 0 : 1;
            }
        }

        Map.MapGenerate(arr).Forget();
    }

    public void SelectPrincess(EPrincessType princessType)
    {
        SelectedPrincessType = princessType;
    }

    private void Main_Update()
    {
        
    }
}